using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    internal class Class1
    {
        public sealed class C_Singelton
        {
            private C_Singelton() { }
            private static SpeechSynthesizer synth;
            // Объект одиночки храниться в статичном поле класса. Существует
            // несколько способов инициализировать это поле, и все они имеют разные
            // достоинства и недостатки. В этом примере мы рассмотрим простейший из
            // них, недостатком которого является полная неспособность правильно
            // работать в многопоточной среде.
            private static C_Singelton _instance;

            // Это статический метод, управляющий доступом к экземпляру одиночки.
            // При первом запуске, он создаёт экземпляр одиночки и помещает его в
            // статическое поле. При последующих запусках, он возвращает клиенту
            // объект, хранящийся в статическом поле.
            public static C_Singelton GetInstance()
            {
                if (_instance == null)
                {
                    _instance = new C_Singelton();
                    synth = new SpeechSynthesizer();
                    synth.SetOutputToDefaultAudioDevice();
                    var voices = synth.GetInstalledVoices(new CultureInfo("ru-RU"));
                    //synth.SelectVoice("Microsoft Server Speech Text to Speech Voice (ru-RU, Elena)");
                    synth.SelectVoice(voices[0].VoiceInfo.Name);
                }
                return _instance;
            }

            // Наконец, любой одиночка должен содержать некоторую бизнес-логику,
            // которая может быть выполнена на его экземпляре.
            public void someBusinessLogic(string Text_for_speak)
            {

                synth.Speak(Text_for_speak);

            }

        }
    }
}

