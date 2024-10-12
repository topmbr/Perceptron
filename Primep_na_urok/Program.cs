using System;

class Perceptron
{
    private double[] weights;
    private double learningRate;
    private double threshold;

    public Perceptron(int inputSize, double learningRate = 0.1, double threshold = 0.5)
    {
        weights = new double[inputSize];
        this.learningRate = learningRate;
        this.threshold = threshold;

        // Ініціалізація ваг випадковими значеннями
        Random rand = new Random();
        for (int i = 0; i < weights.Length; i++)
        {
            weights[i] = rand.NextDouble();
        }
    }

    public int Predict(int[] inputs)
    {
        double sum = 0;
        for (int i = 0; i < inputs.Length; i++)
        {
            sum += inputs[i] * weights[i];
        }

        return sum >= threshold ? 1 : 0;
    }

    public void Train(int[][] inputs, int[] outputs, int epochs)
    {
        for (int e = 0; e < epochs; e++)
        {
            for (int i = 0; i < inputs.Length; i++)
            {
                int prediction = Predict(inputs[i]);
                int error = outputs[i] - prediction; // Розрахунок помилки

                // Логіка навчання: якщо є помилка
                if (error != 0)
                {
                    for (int j = 0; j < weights.Length; j++)
                    {
                        // Оновлення ваги
                        weights[j] += learningRate * error * inputs[i][j];
                    }
                }
            }
        }
    }

    public void PrintWeights()
    {
        Console.WriteLine("Ваги:");
        foreach (var weight in weights)
        {
            Console.WriteLine(weight);
        }
    }
}

class Program
{
    static void Main()
    {
        // Вхідні дані з двома параметрами для операції AND
        int[][] inputs = {
            new int[] {0, 0}, // Приклад 1
            new int[] {0, 1}, // Приклад 2
            new int[] {1, 0}, // Приклад 3
            new int[] {1, 1}  // Приклад 4
        };

        // Вихідні дані для логічної операції AND
        int[] outputs = { 0, 0, 0, 1 }; // Виходи для прикладів AND

        // Створюємо перцептрон
        Perceptron perceptron = new Perceptron(2);

        // Навчаємо перцептрон
        perceptron.Train(inputs, outputs, 100);

        // Виводимо заголовки
        Console.WriteLine("Ввод\t\tВивод");
        Console.WriteLine("ПРИКЛАД\t x1  x2  Результат");

        // Виводимо наявні дані
        for (int i = 0; i < outputs.Length; i++) // Виводимо 4 відомі приклади
        {
            Console.WriteLine($"ПРИКЛАД {i + 1}\t  {inputs[i][0]}   {inputs[i][1]}\t  {outputs[i]}");
        }

        // Перевіряємо нові дані (приклад 5)
        int[] newInput = new int[] { 1, 1 }; // Новий приклад
        int result = perceptron.Predict(newInput);

        // Виводимо новий приклад
        Console.WriteLine($"ПРИКЛАД 5\t  {newInput[0]}   {newInput[1]}\t  {result}");

        // Виводимо ваги для аналізу
        Console.WriteLine();
        perceptron.PrintWeights();
    }
}
