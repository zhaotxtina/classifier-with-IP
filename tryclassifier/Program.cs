using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
////using Accord.MachineLearning;
using Accord;
using Accord.Math;
using Accord.Controls;
//using Accord.MachineLearning.DecisionTrees;
////using Accord.Statistics.Filters;
//using Accord.MachineLearning.DecisionTrees.Learning;
//using Accord.IO;
//using Accord.Controls;
//using Accord.Statistics.Distributions.Univariate;
using Accord.MachineLearning.Bayes;
using Accord.MachineLearning.VectorMachines.Learning;
////using Accord.Neuro;
////using Accord.Neuro.Learning;
//using Accord.Statistics.Models.Regression.Fitting;
//using Accord.Statistics.Models.Regression;
using Accord.Statistics;
//using Accord.Statistics.Analysis;  

namespace trycluster
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Read in Data
            List<float> gr = new List<float>();
            List<float> rdeep = new List<float>();
            List<float> rhob = new List<float>();
            List<float> nphil = new List<float>();
            List<float> dtc = new List<float>();
            List<float> pe = new List<float>();
            List<float> rhobBadFlag = new List<float>();
            List<float> nphilBadFlag = new List<float>();
            List<float> dtcBadFlag = new List<float>();
            List<float> peBadFlag = new List<float>();
            List<float> depth = new List<float>();
            List<float> cal = new List<float>();
            List<float> drho = new List<float>();
            List<float> rugosity = new List<float>();

            using (var reader = new StreamReader("TestData\\gr.txt"))
            {
                while (!reader.EndOfStream)
                {
                    gr.Add(float.Parse(reader.ReadLine()));
                }
            }

            using (var reader = new StreamReader("TestData\\rdeep.txt"))
            {
                while (!reader.EndOfStream)
                {
                    rdeep.Add(float.Parse(reader.ReadLine()));
                }
            }

            using (var reader = new StreamReader("TestData\\rhob.txt"))
            {
                while (!reader.EndOfStream)
                {
                    rhob.Add(float.Parse(reader.ReadLine()));
                }
            }

            using (var reader = new StreamReader("TestData\\nphil.txt"))
            {
                while (!reader.EndOfStream)
                {
                    nphil.Add(float.Parse(reader.ReadLine()));
                }
            }

            using (var reader = new StreamReader("TestData\\dtc.txt"))
            {
                while (!reader.EndOfStream)
                {
                    dtc.Add(float.Parse(reader.ReadLine()));
                }
            }

            using (var reader = new StreamReader("TestData\\pe.txt"))
            {
                while (!reader.EndOfStream)
                {
                    pe.Add(float.Parse(reader.ReadLine()));
                }
            }

            using (var reader = new StreamReader("TestData\\rhobBadFlag.txt"))
            {
                while (!reader.EndOfStream)
                {
                    rhobBadFlag.Add(float.Parse(reader.ReadLine()));
                }
            }

            using (var reader = new StreamReader("TestData\\nphilBadFlag.txt"))
            {
                while (!reader.EndOfStream)
                {
                    nphilBadFlag.Add(float.Parse(reader.ReadLine()));
                }
            }

            using (var reader = new StreamReader("TestData\\dtcBadFlag.txt"))
            {
                while (!reader.EndOfStream)
                {
                    dtcBadFlag.Add(float.Parse(reader.ReadLine()));
                }
            }

            using (var reader = new StreamReader("TestData\\peBadFlag.txt"))
            {
                while (!reader.EndOfStream)
                {
                    peBadFlag.Add(float.Parse(reader.ReadLine()));
                }
            }

            using (var reader = new StreamReader("TestData\\depth.txt"))
            {
                while (!reader.EndOfStream)
                {
                    depth.Add(float.Parse(reader.ReadLine()));
                }
            }

            using (var reader = new StreamReader("TestData\\cal.txt"))
            {
                while (!reader.EndOfStream)
                {
                    cal.Add(float.Parse(reader.ReadLine()));
                }
            }

            using (var reader = new StreamReader("TestData\\rugosity.txt"))
            {
                while (!reader.EndOfStream)
                {
                    rugosity.Add(float.Parse(reader.ReadLine()));
                }
            }

            using (var reader = new StreamReader("TestData\\drho.txt"))
            {
                while (!reader.EndOfStream)
                {
                    drho.Add(float.Parse(reader.ReadLine()));
                }
            }



            #endregion

            // Begin actual code to go into the module
            var data = new DataTable();
            // Data
            //data.Columns.Add(Constants.depth.ToString(), typeof(float));
            //data.Columns.Add(Constants.curveName.gr.ToString(), typeof(float));
            //data.Columns.Add(Constants.curveName.rdeep.ToString(), typeof(float));
            //data.Columns.Add(Constants.curveName.rhob.ToString(), typeof(float));
            //data.Columns.Add(Constants.curveName.nphil.ToString(), typeof(float));
            //data.Columns.Add(Constants.curveName.dtc.ToString(), typeof(float));
            //data.Columns.Add(Constants.curveName.pe.ToString(), typeof(float));
            //data.Columns.Add(Constants.bhfCurveName.cal.ToString(), typeof(float));
            //data.Columns.Add(Constants.bhfCurveName.drho.ToString(), typeof(float));
            //data.Columns.Add(Constants.curveName.nphilBadFlag.ToString(), typeof(float));
            //data.Columns.Add(Constants.curveName.rhobBadFlag.ToString(), typeof(float));
            //data.Columns.Add(Constants.curveName.peBadFlag.ToString(), typeof(float));
            //data.Columns.Add(Constants.curveName.dtcBadFlag.ToString(), typeof(float));
            data.Columns.Add("depth", "gr", "rdeep", "rhob", "nphil", "dtc", "pe", "cal", "drho", "nphilBF", "rhobBF", "peBF", "dtcBF");
            // read in data to table
            //for (var i = 0; i < gr.Count; i++)
            
            for (var i = 0; i < (int)(gr.Count-0.25* gr.Count); i += 3)  // only pick 10% of the data
            {
                // Build up the dataframe. 0's are inserted for the bad hole flags of GR and RDEEP
                // as we know they are always good, and not available as bad hole flag curves.
                // Give a default priority value of -1 to start with.
                if ((gr[i] != -999) && (rdeep[i] != -999) && (rhob[i] != -999) && (nphil[i] != -999) && (dtc[i] != -999) && (pe[i] != -999)
                    && (cal[i] != -999) && (drho[i] != -999) && (nphilBadFlag[i] != -999) && (rhobBadFlag[i] != -999) && (peBadFlag[i] != -999) && (dtcBadFlag[i] != -999))
                {
                    //rawin[i]= new double[] { gr[i], rhob[i] };
                    data.Rows.Add(depth[i], gr[i], rdeep[i], rhob[i], nphil[i], dtc[i], pe[i],
                        cal[i], drho[i], nphilBadFlag[i], rhobBadFlag[i], peBadFlag[i], dtcBadFlag[i]);
                }
            }
            //Console.WriteLine(data.Rows.Count);
            //double[][] rawin = new double[data.Rows.Count][];
            //int[] rawout = new int[data.Rows.Count];
            //for (var i = 0; i < (int)(gr.Count - 0.25 * gr.Count); i += 3)  // only pick 10% of the data
            //{
                
            //    if ((gr[i] != -999) && (rdeep[i] != -999) && (rhob[i] != -999) && (nphil[i] != -999) && (dtc[i] != -999) && (pe[i] != -999)
            //        && (cal[i] != -999) && (drho[i] != -999) && (nphilBadFlag[i] != -999) && (rhobBadFlag[i] != -999) && (peBadFlag[i] != -999) && (dtcBadFlag[i] != -999))
            //    {
            //        rawin[i]=  new double[] { gr[i], rhob[i] };
            //        //rawout[i] =  new int[]  rhobBadFlag[i];
            //    }
            //}
            var datatest = new DataTable();
            datatest.Columns.Add("depth", "gr", "rdeep", "rhob", "nphil", "dtc", "pe", "cal", "drho", "nphilBF", "rhobBF", "peBF", "dtcBF");
            for (var i = (gr.Count - (int)(0.25 * gr.Count)); i < gr.Count; i += 3)  // only pick 10% of the data
            {
                // Build up the dataframe. 0's are inserted for the bad hole flags of GR and RDEEP
                // as we know they are always good, and not available as bad hole flag curves.
                // Give a default priority value of -1 to start with.
                if ((gr[i] != -999) && (rdeep[i] != -999) && (rhob[i] != -999) && (nphil[i] != -999) && (dtc[i] != -999) && (pe[i] != -999)
                    && (cal[i] != -999) && (drho[i] != -999) && (nphilBadFlag[i] != -999) && (rhobBadFlag[i] != -999) && (peBadFlag[i] != -999) && (dtcBadFlag[i] != -999))
                {
                    datatest.Rows.Add(depth[i], gr[i], rdeep[i], rhob[i], nphil[i], dtc[i], pe[i],
                        cal[i], drho[i], nphilBadFlag[i], rhobBadFlag[i], peBadFlag[i], dtcBadFlag[i]);
                }
            }

            

            double[][] inputs = data.ToJagged<double>("gr", "rhob");
            int[] outputs = data.Columns["rhobBF"].ToArray<int>();

            ScatterplotBox.Show("rhobbadflag vs gr and rhob training data", inputs, outputs).Hold();

            //double[][] inputtest = datatest.ToJagged<double>("gr", "rhob");
            //int[] outputtest = datatest.Columns["rhobBF"].ToArray<int>();

            //ScatterplotBox.Show("rhobbadflag vs gr and rhob testing data", inputtest, outputtest).Hold();

            #region naive bayes  working
            //////// Start to build a simple classification job

            ////double[][] inputs = data.ToJagged<double>("gr", "rhob");
            ////int[] outputs = data.Columns["rhobBF"].ToArray<int>();

            ////ScatterplotBox.Show("classification example", inputs, outputs).Hold();

            //// Naive Bayes
            //// Create a Naive Bayes learning algorithm
            //var teacher = new NaiveBayesLearning<NormalDistribution>();

            //// Use the learning algorithm to learn
            //var nb = teacher.Learn(inputs, outputs);

            //// At this point, the learning algorithm should have
            //// figured important details about the problem itself:
            //int numberOfClasses = nb.NumberOfClasses; // should be 2 (positive or negative)
            //int nunmberOfInputs = nb.NumberOfInputs;  // should be 2 (x and y coordinates)

            //// Classify the samples using the model
            //int[] answers = nb.Decide(inputs);

            //// Plot the results
            ////ScatterplotBox.Show("Training Data", inputs, outputs);
            ////ScatterplotBox.Show("Naive Bayes Prediction for training data", inputs, answers).Hold();

            //// Classify the samples using the model
            //int[] answers2 = nb.Decide(inputtest);

            //// Plot the results
            ////ScatterplotBox.Show("Test Data", inputtest, outputtest);
            ////ScatterplotBox.Show("Naive Bayes Prediction for test data", inputtest, answers2).Hold();

            //// Works perfect
            #endregion

            #region SVM not working yet

            //  SVMs are supervised learning models with associated learning algorithms that analyze data and recognize patterns, used for classification and regression analysis.
            //  Linear In the Linear SVM the idea is to design a hyperplane that classifies the training vectors in two classes.

            // Create a L2-regularized L2-loss optimization algorithm for
            // the dual form of the learning problem. This is *exactly* the
            // same method used by LIBLINEAR when specifying -s 1 in the 
            // command line (i.e. L2R_L2LOSS_SVC_DUAL).
            //
            var teacher2 = new LinearCoordinateDescent();

            // Teach the vector machine
            var svm = teacher2.Learn(inputs, outputs);

            // Classify the samples using the model
            bool[] answers2 = svm.Decide(inputs);

            // Convert to Int32 so we can plot:
            int[] zeroOneAnswers2 = answers2.ToZeroOne();

            //// Plot the results
            //ScatterplotBox.Show("Expected results", inputs, outputs);
            //ScatterplotBox.Show("LinearSVM results", inputs, zeroOneAnswers2);

            //// Grab the index of multipliers higher than 0
            //int[] idx = teacher.Lagrange.Find(x => x > 0);

            //// Select the input vectors for those
            //double[][] sv = inputs.Get(idx);

            //// Plot the support vectors selected by the machine
            //ScatterplotBox.Show("Support vectors", sv).Hold();

            #endregion

            #region Decision Tree  working

            //// In our problem, we have 2 classes (samples can be either
            //// positive or negative), and 2 continuous-valued inputs.

            //C45Learning teacher3 = new C45Learning(new[] {
            //    DecisionVariable.Continuous("gr"),
            //    DecisionVariable.Continuous("rhob")
            //    });

            //// Use the learning algorithm to induce the tree
            //DecisionTree tree = teacher3.Learn(inputs, outputs);

            //// Classify the samples using the model
            //int[] answers3 = tree.Decide(inputs);

            ////// Plot the results
            ////ScatterplotBox.Show("Expected results", inputs, outputs);
            ////ScatterplotBox.Show("Decision Tree results", inputs, answers3).Hold();

            //// Plot the results
            ////ScatterplotBox.Show("Training Data", inputs, outputs);
            ////ScatterplotBox.Show("Decision Tree Prediction for training data", inputs, answers3).Hold();

            //// Classify the samples using the model
            //int[] answers4 = tree.Decide(inputtest);

            //// Plot the results
            ////ScatterplotBox.Show("Test Data", inputtest, outputtest);
            ////ScatterplotBox.Show("Decision Tree Prediction for test data", inputtest, answers4).Hold();

            #endregion

            #region Neural network not working yet
            ////double[][] inputs = data.ToJagged<double>("gr", "rhob");
            ////int[] outputs = data.Columns["rhobBF"].ToArray<int>();

            //ScatterplotBox.Show("claasification example", inputs, outputs).Hold();
            // Since we would like to learn binary outputs in the form
            // [-1,+1], we can use a bipolar sigmoid activation function
            ////IActivationFunction function = new BipolarSigmoidFunction();
            //IActivationFunction function = new SigmoidFunction();
            //// In our problem, we have 2 inputs (x, y pairs), and we will 
            //// be creating a network with 5 hidden neurons and 1 output:
            ////
            //var network = new ActivationNetwork(function,
            //    inputsCount: 2, neuronsCount: new[] { 10, 1 });

            //// Create a Levenberg-Marquardt algorithm
            //var teacher = new LevenbergMarquardtLearning(network)
            //{
            //    UseRegularization = true
            //};

            //// Because the network is expecting multiple outputs,
            //// we have to convert our single variable into arrays
            ////
            //var y = outputs.ToDouble().ToJagged();

            //// Iterate until stop criteria is met
            ////double error = double.PositiveInfinity;
            ////double previous;

            ////do
            ////{
            ////    previous = error;

            ////    // Compute one learning iteration
            ////    error = teacher.RunEpoch(inputs, y);

            ////} while (Math.Abs(previous - error) < 1e-10 * previous);
            //int ii = 1;
            //double error = 1.0;
            //while (ii < 1000)
            //{
            //    // teach the network
            //    error = teacher.RunEpoch(inputs, y);
            //    //Console.WriteLine($"Iteration={ii}. Error={error}");
            //    ii++;
            //}

            //// Classify the samples using the model
            //int[] answers = inputs.Apply(network.Compute).GetColumn(0).Apply(System.Math.Sign);

            //// Plot the results
            //ScatterplotBox.Show("Expected results", inputs, outputs);
            //ScatterplotBox.Show("Network results", inputs, answers)
            //.Hold();


            #endregion

            #region logistic regression works

            //// Create iterative re-weighted least squares for logistic regressions
            //var teacher4 = new IterativeReweightedLeastSquares<LogisticRegression>()
            //{
            //    MaxIterations = 100,
            //    Regularization = 1e-6
            //};

            //// Use the teacher algorithm to learn the regression:
            //LogisticRegression lr = teacher4.Learn(inputs, outputs);

            //// Classify the samples using the model
            //bool[] answers5 = lr.Decide(inputs);

            //// Convert to Int32 so we can plot:
            //int[] zeroOneAnswers = answers5.ToZeroOne();

            //// Plot the results
            ////scatterplotBox.Show("Training Data", inputs, outputs);
            ////scatterplotBox.Show("Logistic Regression Prediction for training data", inputs, zeroOneAnswers).Hold();


            //// Classify the samples using the model

            //bool[] answers6 = lr.Decide(inputtest);

            //// Convert to Int32 so we can plot:
            //int[] zeroOneAnswers2 = answers6.ToZeroOne();

            //// Plot the results
            ////scatterplotBox.Show("Test Data", inputtest, outputtest);
            ////scatterplotBox.Show("Logistic Regression Prediction for test data", inputtest, zeroOneAnswers2).Hold();

            #endregion

            #region confusion matrix evaluation
            //// Let's say we have a decision problem involving 2 classes. In a typical
            //// machine learning problem, have a set of expected, ground truth values:
            //// Let's compar the three prediction results 
            //int[] expected = outputtest;  // only one expected value


            //// And we have a set of values that have been predicted by a machine model:
            //// 
            //int[] predicted = answers2; // naive bayes
            //int[] predicted2 = answers4; //decision tree
            //int[] predicted3 = zeroOneAnswers2; //logistic regression

            //// We can get different performance measures to assess how good our model was at 
            //// predicting the true, expected, ground-truth labels for the decision problem:
            //var cm = new GeneralConfusionMatrix(classes: 2, expected: expected, predicted: predicted);
            //var cm2 = new GeneralConfusionMatrix(classes: 2, expected: expected, predicted: predicted2);
            //var cm3 = new GeneralConfusionMatrix(classes: 2, expected: expected, predicted: predicted3);

            //// We can obtain the proper confusion matrix using:
            //int[,] matrix = cm.Matrix;
            //int[,] matrix2 = cm2.Matrix;
            //int[,] matrix3 = cm3.Matrix;


            //// We can get more information about our problem as well:
            //int classes = cm.NumberOfClasses; // should be 2
            //int samples = cm.NumberOfSamples; // should be 1400
            //int classes2 = cm2.NumberOfClasses; // should be 2
            //int samples2 = cm2.NumberOfSamples; // should be 1400
            //int classes3 = cm3.NumberOfClasses; // should be 2
            //int samples3 = cm3.NumberOfSamples; // should be 1400

            //// And multiple performance measures:
            //List<double> accuracy = new List<double>();
            //List<double> error = new List<double>();
            //List<double> chanceAgreement = new List<double>();
            //List<double> geommetricAgreement = new List<double>();
            //List<double> FScore = new List<double>();
            //List<double> pearson = new List<double>();
            //List<double> kappa = new List<double>();
            //List<double> tau = new List<double>();
            //List<double> chiSquare = new List<double>();
            //List<double> kappaStdErr = new List<double>();
            //List<double> kappaStdErr0 = new List<double>();

            //accuracy.Add(cm.Accuracy);                      //  higher the better
            //accuracy.Add(cm2.Accuracy);
            //accuracy.Add(cm3.Accuracy);
            //error.Add(cm.Error);                            //  lower the better
            //error.Add(cm2.Error);                            //  lower the better
            //error.Add(cm3.Error);                            //  lower the better
            //chanceAgreement.Add(cm.ChanceAgreement);        // 
            //chanceAgreement.Add(cm2.ChanceAgreement);        // 
            //chanceAgreement.Add(cm3.ChanceAgreement);        // 
            //geommetricAgreement.Add(cm.GeometricAgreement); //  0 (the classifier completely missed one class)
            //geommetricAgreement.Add(cm2.GeometricAgreement); //  0 (the classifier completely missed one class)
            //geommetricAgreement.Add(cm3.GeometricAgreement); //  0 (the classifier completely missed one class)
            ////FScore.Add(cm.FScore);                        // 
            ////FScore.Add(cm2.FScore);                        // 
            ////FScore.Add(cm3.FScore);
            //pearson.Add(cm.Pearson);                        // 
            //pearson.Add(cm2.Pearson);                        // 
            //pearson.Add(cm3.Pearson);                        // 
            //kappa.Add(cm.Kappa);                            // 
            //kappa.Add(cm2.Kappa);                            // 
            //kappa.Add(cm3.Kappa);                            // 
            //tau.Add(cm.Tau);                                //
            //tau.Add(cm2.Tau);
            //tau.Add(cm3.Tau);
            //chiSquare.Add(cm.ChiSquare);                    // 
            //chiSquare.Add(cm2.ChiSquare);                    // 
            //chiSquare.Add(cm3.ChiSquare);                    // 

            //// and some of their standard errors:
            //kappaStdErr.Add(cm.StandardError);
            //kappaStdErr.Add(cm2.StandardError);
            //kappaStdErr.Add(cm3.StandardError);    // 
            //kappaStdErr0.Add(cm.StandardErrorUnderNull);    // 
            //kappaStdErr0.Add(cm2.StandardErrorUnderNull);    // 
            //kappaStdErr0.Add(cm3.StandardErrorUnderNull);    // 

            //Console.WriteLine(accuracy);
            #endregion

            #region an example on NN
            //// Read the Excel worksheet into a DataTable
            //DataTable table = new ExcelReader("TestData\\examples.xls").GetWorksheet("Classification - Yin Yang");

            //// Convert the DataTable to input and output vectors
            //double[][] inputs = table.ToJagged<double>("X", "Y");
            //int[] outputs = table.Columns["G"].ToArray<int>();

            //// Plot the data
            //ScatterplotBox.Show("Yin-Yang", inputs, outputs).Hold();

            //// Since we would like to learn binary outputs in the form
            //// [-1,+1], we can use a bipolar sigmoid activation function
            //IActivationFunction function = new BipolarSigmoidFunction();

            //// In our problem, we have 2 inputs (x, y pairs), and we will 
            //// be creating a network with 5 hidden neurons and 1 output:
            ////
            //var network = new ActivationNetwork(function,
            //    inputsCount: 2, neuronsCount: new[] { 5, 1 });

            //// Create a Levenberg-Marquardt algorithm
            //var teacher = new LevenbergMarquardtLearning(network)
            //{
            //    UseRegularization = true
            //};

            //// Because the network is expecting multiple outputs,
            //// we have to convert our single variable into arrays
            ////
            //var y = outputs.ToDouble().ToArray();

            //// Iterate until stop criteria is met
            //double error = double.PositiveInfinity;
            //double previous;

            //do
            //{
            //    previous = error;

            //    // Compute one learning iteration
            //    error = teacher.RunEpoch(inputs, y);

            //} while (Math.Abs(previous - error) < 1e-10 * previous);


            //// Classify the samples using the model
            //int[] answers = inputs.Apply(network.Compute).GetColumn(0).Apply(System.Math.Sign);

            //// Plot the results
            //ScatterplotBox.Show("Expected results", inputs, outputs);
            //ScatterplotBox.Show("Network results", inputs, answers)
            //.Hold();
            #endregion
        }
    }
}
