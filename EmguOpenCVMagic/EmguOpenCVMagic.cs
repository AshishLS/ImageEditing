using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Features2D;
using Emgu.CV.Flann;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV.CvEnum;
using System.Diagnostics;
using System.Windows.Forms;
using Emgu.CV.UI;

namespace EmguOpenCVMagic
{
    public class EmguOpenCVMagic
    {
        public int TestMethod()
        {
            // http://stackoverflow.com/questions/11541154/checking-images-for-similarity-with-opencv
            return 0;
        }
    }

    public static class DrawMatches
    {
        /// <summary>
        /// Checks the two images and calculates similarity percentage.
        /// </summary>
        /// <param name="baseImagePath"></param>
        /// <param name="currentImagePath"></param>
        /// <returns>percentage of similarity</returns>
        public static double MatchImages(string baseImagePath, string currentImagePath, out Bitmap resultImg)
        {
            long matchTime;
            double matchPercentage = 0.00;
            using (Mat baseImage = CvInvoke.Imread(baseImagePath.ToLower(), ImreadModes.Color))
            using (Mat currentImage = CvInvoke.Imread(currentImagePath.ToLower(), ImreadModes.Color))
            {
                Mat result = Draw(baseImage, currentImage, out matchTime, out matchPercentage);
                resultImg = new Bitmap(result.Bitmap);
                //ImageViewer.Show(result, String.Format("Matched in {0} milliseconds", matchTime));
                result.Dispose();
                baseImage.Dispose();
                currentImage.Dispose();
            }
            return matchPercentage;
        }
        private static void FindMatch(Mat modelImage, Mat observedImage, out long matchTime, out VectorOfKeyPoint modelKeyPoints, out VectorOfKeyPoint observedKeyPoints, VectorOfVectorOfDMatch matches, out Mat mask, out Mat homography)
        {
            int k = 2;
            double uniquenessThreshold = 0.80;

            Stopwatch watch;
            homography = null;

            modelKeyPoints = new VectorOfKeyPoint();
            observedKeyPoints = new VectorOfKeyPoint();

            try
            {
                using (UMat uModelImage = modelImage.GetUMat(AccessType.Read))
                using (UMat uObservedImage = observedImage.GetUMat(AccessType.Read))
                {
                    KAZE featureDetector = new KAZE();

                    //extract features from the object image
                    Mat modelDescriptors = new Mat();
                    featureDetector.DetectAndCompute(uModelImage, null, modelKeyPoints, modelDescriptors, false);

                    watch = Stopwatch.StartNew();

                    // extract features from the observed image
                    Mat observedDescriptors = new Mat();
                    featureDetector.DetectAndCompute(uObservedImage, null, observedKeyPoints, observedDescriptors, false);

                    // Bruteforce, slower but more accurate
                    // You can use KDTree for faster matching with slight loss in accuracy
                    using (Emgu.CV.Flann.LinearIndexParams ip = new Emgu.CV.Flann.LinearIndexParams())
                    using (Emgu.CV.Flann.SearchParams sp = new SearchParams())
                    using (DescriptorMatcher matcher = new FlannBasedMatcher(ip, sp))
                    {
                        matcher.Add(modelDescriptors);

                        matcher.KnnMatch(observedDescriptors, matches, k, null);
                        mask = new Mat(matches.Size, 1, DepthType.Cv8U, 1);
                        mask.SetTo(new MCvScalar(255));
                        Features2DToolbox.VoteForUniqueness(matches, uniquenessThreshold, mask);

                        int nonZeroCount = CvInvoke.CountNonZero(mask);
                        if (nonZeroCount >= 4)
                        {
                            nonZeroCount = Features2DToolbox.VoteForSizeAndOrientation(modelKeyPoints, observedKeyPoints,
                                matches, mask, 1.5, 20);
                            if (nonZeroCount >= 4)
                                homography = Features2DToolbox.GetHomographyMatrixFromMatchedFeatures(modelKeyPoints,
                                    observedKeyPoints, matches, mask, 2);
                        }
                    }
                    watch.Stop();

                }
            }
            catch (Exception e)
            {
                throw e;
            }
            
            matchTime = watch.ElapsedMilliseconds;
        }

        /// <summary>
        /// Draw the model image and observed image, the matched features and homography projection.
        /// </summary>
        /// <param name="modelImage">The model image</param>
        /// <param name="observedImage">The observed image</param>
        /// <param name="matchTime">The output total time for computing the homography matrix.</param>
        /// <returns>The model image and observed image, the matched features and homography projection.</returns>
        private static Mat Draw(Mat modelImage, Mat observedImage, out long matchTime, out double matchPercentage)
        {
            matchPercentage = 0.0;
            Mat homography;
            VectorOfKeyPoint modelKeyPoints;
            VectorOfKeyPoint observedKeyPoints;
            using (VectorOfVectorOfDMatch matches = new VectorOfVectorOfDMatch())
            {
                Mat mask;
                FindMatch(modelImage, observedImage, out matchTime, out modelKeyPoints, out observedKeyPoints, matches,
                   out mask, out homography);

                var difference = Math.Abs(modelKeyPoints.Size - observedKeyPoints.Size);
                if (modelKeyPoints.Size > 0)
                {
                    matchPercentage = 100.0 * ((double)difference / (double)modelKeyPoints.Size);
                    //MessageBox.Show(String.Format("The images are {0}% different", matchPercentage));
                }
                else
                {
                    MessageBox.Show(String.Format("No keypoints in model image. Must be a blank image"));
                }

                //Draw the matched keypoints
                Mat result = new Mat();
                Features2DToolbox.DrawMatches(modelImage, modelKeyPoints, observedImage, observedKeyPoints,
                   matches, result, new MCvScalar(255, 255, 255), new MCvScalar(255, 255, 255), mask);

                #region draw the projected region on the image

                if (homography != null)
                {
                    //draw a rectangle along the projected model
                    Rectangle rect = new Rectangle(Point.Empty, modelImage.Size);
                    PointF[] pts = new PointF[]
                    {
                  new PointF(rect.Left, rect.Bottom),
                  new PointF(rect.Right, rect.Bottom),
                  new PointF(rect.Right, rect.Top),
                  new PointF(rect.Left, rect.Top)
                    };
                    pts = CvInvoke.PerspectiveTransform(pts, homography);

#if NETFX_CORE
               Point[] points = Extensions.ConvertAll<PointF, Point>(pts, Point.Round);
#else
                    Point[] points = Array.ConvertAll<PointF, Point>(pts, Point.Round);
#endif
                    using (VectorOfPoint vp = new VectorOfPoint(points))
                    {
                        CvInvoke.Polylines(result, vp, true, new MCvScalar(255, 0, 0, 255), 5);
                    }
                }
                #endregion

                return result;

            }
        }
    }
}
