using System;
using System.Diagnostics;


namespace NumberGuesser
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var lox = new[]
            {
                "{0}, ты опять не прав, чушка",
                "Господи иисусе, откуда берутся такие дауны как ты, {0}",
                "{0},я никогда не видел никого тупее тебя, пёс",
                "Ну ты и е*ырок, {0}",
                "Ты просто конченный, {0}",
                "{0}, уйди отсюда, убогий"
            };

            Console.WriteLine("Представьтесь, пожалуйста");
            var name = Console.ReadLine();

            int[] history = new int[1000];

            var number = new Random().Next(0, 101);
            Console.WriteLine("Приветствую {0}! Я загадал число от 0 до 100.\nПопробуй его отгадать! " +
                              "Время пошло!\nВводи числа", name);
            var strike = 0;

            var sWatch = new Stopwatch();
            sWatch.Start();

            while (true)
            {
                var enter = Console.ReadLine();
                if (enter != null && enter.Equals("q"))
                {
                    Console.WriteLine("Извините, я закроюсь, когда вы нажмёте Enter");
                    Console.ReadLine();
                    break;
                }

                var variant = 0;

                if (!int.TryParse(enter, out variant))
                {
                    Console.WriteLine("Ты даже не можешь ввести число. Я не хочу с тобой играть!");
                    Console.Read();
                    break;
                }


                history[strike] = variant;
                ++strike;

                if (variant < number)
                {
                    Console.WriteLine("Твоё число меньше моего");
                    if (strike % 4 == 0)
                    {
                        Console.WriteLine(lox[new Random().Next(0, 6)], name);
                    }
                }
                if (variant > number)
                {
                    Console.WriteLine("Твоё число больше моего");
                    if (strike % 4 == 0)
                    {
                        Console.WriteLine(lox[new Random().Next(0, 6)], name);
                    }
                }
                if (variant != number) continue;

                sWatch.Stop();

                Console.WriteLine("Ну чтож. Ты угадал. Нажми Enter для закрытия");
                Console.WriteLine("Всего попыток {0}", strike);
                Console.WriteLine("Ты вводил:");

                for (var i = 0; i < strike - 1; ++i)
                {
                    Console.Write(history[i]);
                    Console.Write(" ");
                    Console.WriteLine(history[i] < number ? "<" : ">");
                }

                var tSpan = sWatch.Elapsed;

                Console.WriteLine("Потрачено времени: {0} минут, {1} секунд", tSpan.Minutes, tSpan.Seconds);

                Console.ReadLine();
                break;
            }
        }
    }
}