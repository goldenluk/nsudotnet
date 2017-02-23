using System;


namespace NumberGuesser
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string [] lox = new string[6];

            Console.WriteLine("Представьтесь, пожалуйста");
            var name = Console.ReadLine();

            int [] history = new int[1000];

            lox[0] = string.Format("{0}, ты опять не прав, чушка", name);
            lox[1] = string.Format("Господи иисусе, откуда берутся такие дауны как ты, {0}", name);
            lox[2] = string.Format("{0},я никогда не видел никого тупее тебя, пёс", name);
            lox[3] = string.Format("Ну ты и е*ырок, {0}", name);
            lox[4] = string.Format("Ты просто конченный, {0}", name);
            lox[5] = string.Format("{0}, уйди отсюда, убогий", name);

            var number = new Random().Next(0, 101);
            Console.WriteLine("Приветствую {0}! Я загадал число от 0 до 100.\nПопробуй его отгадать! "+
                              "Время пошло!\nВводи числа", name);
            var strike = 0;
            var start = DateTime.Now;

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
                try
                {
                    variant = int.Parse(enter);

                }
                catch (Exception e)
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
                        Console.WriteLine(lox [new Random().Next(0,6)]);
                    }

                }
                if (variant > number)
                {
                    Console.WriteLine("Твоё число больше моего");
                    if (strike % 4 == 0)
                    {
                        Console.WriteLine(lox[new Random().Next(0, 7)]);
                    }
                }
                if (variant != number) continue;

                var end = DateTime.Now;

                Console.WriteLine("Ну чтож. Ты угадал. Нажми Enter для закрытия");
                Console.WriteLine("Всего попыток {0}", strike);
                Console.WriteLine("Ты вводил:");

                for (var i = 0; i < strike -1; ++i)
                {
                    Console.Write(history[i]);
                    Console.Write(" ");
                    Console.WriteLine(history[i] < number ? "<" : ">");
                }

                Console.WriteLine("Потрачено времени: {0} минут, {1} секунд", end.Minute - start.Minute, end.Second - start.Second);

                Console.ReadLine();
                break;
            }
        }
    }
}