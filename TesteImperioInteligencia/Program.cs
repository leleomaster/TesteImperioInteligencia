using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteImperioInteligencia
{
    class Program
    {
        static void Main(string[] args)
        {
            string data = "01/03/2010 23:00";
            long minutos = 15475;

            long horas = minutos / (24 * 60); // 18

            long dias = minutos / 1440; // 2

            Console.WriteLine(SomarData(data, minutos));

            Console.WriteLine($"\n\n{data}  -  {minutos} minutos, {horas} - horas, {dias} - dias\n\n");

            Console.WriteLine(SubtrairData(data, minutos));

            // ConverterDataHoraEmMinutos("01/03/2010 23:55");
            Console.ReadKey();
        }

        /// <summary>
        /// Método para mudar a Data e Hora
        /// </summary>
        /// <param name="date">  é uma data em forma de String formatada no padão “dd/MM/yyyy HH24:mi”</param>
        /// <param name="op">    operação, só poderá aceitar os caracteres ‘+’ e ‘-’</param>
        /// <param name="value"> valor em minutos que deve ser acrescentado ou decrementado</param>
        /// <returns></returns>
        public static string ChangeDate(string date, char op, long value)
        {

            string data = string.Empty;

            if (op == '+')
            {
                data = SomarData(date, value);
            }
            else if (op == '-')
            {
                data = SubtrairData(date, value);
            }
            else
            {
                data = "";
            }

            return data;
        }

        public static string SomarData(string date, long value)
        {
            long dias = value / 1440;
            long mes = Convert.ToInt64(date.Substring(3, 2));
            long ano = Convert.ToInt64(date.Substring(6, 4));

            long horas = (value - (dias * 24 * 60)) / 60;
            long minutos = value - ((dias * 24 * 60) + (horas * 60));

            minutos += Convert.ToInt64(date.Substring(14, 2));
            horas += Convert.ToInt64(date.Substring(11, 2));
            dias += Convert.ToInt64(date.Substring(0, 2));

            if (minutos >= 60)
            {
                horas += minutos / 60;
                minutos = minutos %= 60;
            }

            if (horas >= 24)
            {
                dias += horas / 24;
                horas = horas %= 24;
            }

            bool continuar = true;
            do
            {
                long quantidadeDiasDoMes = ObterDiasDoMes(mes);

                if (dias > quantidadeDiasDoMes)
                {
                    mes += dias / quantidadeDiasDoMes;
                    dias = dias %= quantidadeDiasDoMes;
                }
                else
                {
                    continuar = false;
                }

            } while (continuar);

            if (mes >= 12)
            {
                ano += mes / 12;
                mes = mes %= 12;
            }

            string dataHora = $"{dias.ToString().PadLeft(2, '0')}/{mes.ToString().PadLeft(2, '0')}/{ano.ToString().PadLeft(4, '0')} {horas.ToString().PadLeft(2, '0')}:{minutos.ToString().PadLeft(2, '0')}";

            return dataHora;
        }

        public static string SubtrairData(string date, long value)
        {
            long dias = value / 1440; // 2
            long mes = Convert.ToInt64(date.Substring(3, 2));
            long ano = Convert.ToInt64(date.Substring(6, 4));

            long horas = (value - (dias * 24 * 60)) / 60; // 18
            long minutos = value - ((dias * 24 * 60) + (horas * 60)); // 40

            minutos = Convert.ToInt64(date.Substring(14, 2)) - minutos;
            horas = Convert.ToInt64(date.Substring(11, 2)) - horas;
            dias = Convert.ToInt64(date.Substring(0, 2)) - dias;

            if (minutos < 0)
            {
                if (horas < 0)
                {
                    horas = ((-1) * horas) - 1;
                    horas = ((-1) * horas);
                }
                else
                {
                    horas = horas - 1;
                }
                minutos = (minutos * (-1)) - 60;
                minutos = minutos * (-1);
            }

            if (horas < 0)
            {
                dias -= 1;
                horas = horas * (-1);
            }

            bool continuar = true;
            do
            {
                long quantidadeDiasDoMes = ObterDiasDoMes(mes);

                if (dias > quantidadeDiasDoMes)
                {
                    dias -= quantidadeDiasDoMes;
                    mes -= 1;
                    quantidadeDiasDoMes = ObterDiasDoMes(mes);
                }
                else if (dias < 0)
                {
                    mes -= 1;
                    quantidadeDiasDoMes = ObterDiasDoMes(mes);
                    dias = dias + quantidadeDiasDoMes;
                    continuar = false;
                }
                else
                {
                    continuar = false;
                }

                if (mes == 0)
                {
                    ano -= 1;
                    mes = 12;
                    dias = ObterDiasDoMes(mes) - dias;
                }

            } while (continuar);

            if (mes < 0)
            {
                ano -= 1;
                mes = mes * (-1);
                dias = ObterDiasDoMes(mes);
            }

            string dataHora = $"{dias.ToString().PadLeft(2, '0')}/{mes.ToString().PadLeft(2, '0')}/{ano.ToString().PadLeft(4, '0')} {horas.ToString().PadLeft(2, '0')}:{minutos.ToString().PadLeft(2, '0')}";

            return dataHora;
        }

        private static long ObterDiasDoMes(long mes)
        {
            long quantidadeDiasDoMes = 0;

            if (mes == 2)
            {
                quantidadeDiasDoMes = 28;
            }
            else if (mes == 4 || mes == 6 || mes == 9 || mes == 11)
            {
                quantidadeDiasDoMes = 30;
            }
            else
            {
                quantidadeDiasDoMes = 31;
            }

            return quantidadeDiasDoMes;
        }

    }
}
