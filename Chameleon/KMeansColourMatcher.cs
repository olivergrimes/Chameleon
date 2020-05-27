using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chameleon
{
    public class KMeansColourMatcher : IColourMatcher
    {
        private readonly float _maxDistance;

        public KMeansColourMatcher(float maxDistance = 20000)
        {
            _maxDistance = maxDistance;
        }

        public ColourMatch GetClosestMatch(IEnumerable<RGBPixel> sourcePixels, IEnumerable<PaletteOption> targetPalette)
        {
            if (sourcePixels == null || targetPalette == null)
            {
                return null;
            }

            var mlContext = new MLContext(seed: 0);
            var trainingData = mlContext.Data.LoadFromEnumerable(sourcePixels
                .Select(p => new Example(p)));

            var pipeline = mlContext.Clustering.Trainers.KMeans(new KMeansTrainer.Options
            {
                NumberOfClusters = 1,
                OptimizationTolerance = 1e-3f,
                NumberOfThreads = 1,
                MaximumNumberOfIterations = 100
            });

            ClusteringPredictionTransformer<KMeansModelParameters> model;

            try
            {
                model = pipeline.Fit(trainingData);
            }
            catch (InvalidOperationException)
            {
                //Unable to train dataset, usually when too small
                return null;
            }

            VBuffer<float>[] centroids = default;

            var modelParams = model.Model;
            modelParams.GetClusterCentroids(ref centroids, out int k);

            //Get distance from centroid
            var predictor = mlContext.Model.CreatePredictionEngine<Example, Prediction>(model);

            var predictions = targetPalette.Select(colour =>
                new ColourMatch
                {
                    Distance = predictor.Predict(new Example(colour.RGB)).Distances[0],
                    Colour = colour
                });

            var topMatch = predictions
                .Where(p => p.Distance < _maxDistance)
                .OrderBy(p => p.Distance)
                .FirstOrDefault();

            if (topMatch == null)
            {
                return null;
            }

            return topMatch;
        }

        class Prediction
        {
            [ColumnName("PredictedLabel")]
            public uint PredictedClusterId { get; set; }

            [ColumnName("Score")]
            public float[] Distances { get; set; }
        }

        class Example
        {
            [VectorType(3)]
            public float[] Features { get; set; }

            public Example()
            { }

            public Example(RGBPixel rgb)
            {
                Features = new float[]
                {
                    rgb.Red,
                    rgb.Green,
                    rgb.Blue
                };
            }
        }
    }
}
