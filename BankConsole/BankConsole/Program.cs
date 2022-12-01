using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection.Metadata;

namespace BankConsole
{
    enum CurrencyExchange
    {
        GrnToDollar,
        GrnToEuro,
        DollarToGrn,
        EuroToGrn,
        EuroToDollar,
        DollarToEuro
    }

    class Program
    {
        static void Main(string[] args)
        {
            ReportBankSingleton reportBankSingleton1 = ReportBankSingleton.GetInstance();
            CCustomer costumer4 = new CCustomer("Vlad", 25, 12000, 4000, 200);
            CCustomer costumer1 = new CCustomer("Vadim", 25, 12000, 4000, 200);
            CCustomer costumer2 = new CCustomer("Misha", 25, 12000, 4000, 200);
            CCustomer costumer3 = new CCustomer("Kolya", 25, 12000, 4000, 200);
            List<CCustomer> costumers = new List<CCustomer>() { costumer4, costumer1, costumer2, costumer3 };
            Random random = new Random();
            //CCostumer costumer = costumers[random.Next(0, 3)];
        
            Console.WriteLine("Good afternoon, Privatbank welcomes you, please approach a consultant for further actions.\n            (1 - We approach the consultant, 2 - We leave the bank)");
            Console.WriteLine("Your decision: ");//Твоє рішення
            int a = Convert.ToInt32(Console.ReadLine());
            if (a == 1)
            {
                CComputer computer = new CComputer();
                CBank bank = new CBank();
                CClinerMan clinerMan = new CClinerMan("Olya", 18, 2);
                CVinnik vinik = new CVinnik();
                int Hp = vinik.Hp;
                CEmployee employee = new CEmployee("Vasia", 35, computer);
                CConsultant consultant = new CConsultant("Larisa", 20);
                foreach (var item in costumers)
                {
                    Console.WriteLine(item.Name);
                    item.Work(consultant, employee, bank);
                    Console.WriteLine($"Goodbye {item.Name}!");
                }
                
                CBank.ProcentD = clinerMan.Cline(CBank.ProcentD, ref Hp, vinik.Power);
                vinik.Hp = Hp;

                reportBankSingleton1.PrintReport();
                Console.WriteLine(employee.Report);
                
            }
            if (a == 2)
            {
                Console.WriteLine($"Goodbye! ");
            }
        }
    }

   sealed class ReportBankSingleton
    {
        static ReportBankSingleton _instance;
        public static string Report { get; set; }
        private ReportBankSingleton()
        {
        }
        public static ReportBankSingleton GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ReportBankSingleton();
            }
            return _instance;

        }

        public void GetReport(string report)
        {
            Report += report;
        }

        public void PrintReport()
        {
            Console.WriteLine(Report);
        }

    }

    

    class Facade
    {
        public CCustomer CCostumer;

        public Facade(CCustomer CCostumer)
        {
            this.CCostumer = CCostumer;
        }

        public void Operation1(CEmployee cEmployee,CurrencyExchange currency,CCustomer costumer)
        {
            cEmployee.Question2(currency);
            double customerStartMoney = float.Parse(Console.ReadLine());
            double customerResultMoney = cEmployee.Exchange(currency, customerStartMoney,costumer);
            if (customerResultMoney <= cEmployee._dollarAmount)
            {
                Console.WriteLine("You get " + customerStartMoney + " grn and received " + customerResultMoney + " dollars");

            }
            else
            {
                Console.WriteLine("We have not enough money");
            }
        }
        public void Operation2(double procent)
        {
            Console.WriteLine("Enter the amount of your deposit in hryvnias:");//Вкажіть суму вашого вкладу в гривнях
            int Vklad = Convert.ToInt32(Console.ReadLine());
            if (Vklad >= 5000)
            {
                double Prubutok = (Vklad * procent) / 2;
                Console.WriteLine("The profit you will receive at the end of the term of the deposit excluding tax:   " + Prubutok + "grn");//Прибуток, який ви отримаєте по закінченню терміну вкладу без вирахування податку: 
                double PrubutokPod = Prubutok - (Prubutok * 0.19);
                Console.WriteLine("The profit you will receive at the end of the term of the deposit with tax:   " + PrubutokPod + "grn");//Прибуток, який ви отримаєте по закінченню терміну вкладу з податком:
            }
            else
            {
                Console.WriteLine("You cannot make a deposit");// ви не можете зробити депозит
            }
        }

    }

    abstract class CPerson
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public CPerson(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }

    class CCustomer : CPerson
    {
        #region Fields and properties
        private int termin;

        public int Grivnas { get; set; }
        public int Euros { get; set; }
        public int Dollars { get; set; }
        #endregion
                        internal CBank CBank
        {
            get => default;
            set
            {
            }
        }

        #region Constructors
        public CCustomer(string name, int age, int grivnas, int euros, int dollars) : base(name, age)
        {
            Grivnas = grivnas;
            Euros = euros;
            Dollars = dollars;
        }
        #endregion
        #region Methods
        public int Clientdirt()
        {
            Random random = new Random();
            int a = random.Next(1, 5);
            return a;
        }
        public void Work(CConsultant consultant, CEmployee employee, CBank bank)
        {
            var punct = 0;
            var punct1 = 0;
            var punct2 = 0;
            var punct3 = 0;
            var punct4 = 0;
            Facade facade = new Facade(this);
            bool check = true;
            while (check)
            {
                consultant.Consult();
                punct = Convert.ToInt32(Console.ReadLine());
                switch (punct)
                {
                    case 0:
                        break;
                    #region Case1
                    case 1:
                        employee.Question1();
                        punct1 = Convert.ToInt32(Console.ReadLine());
                        switch (punct1)
                        {
                            case 0:
                                Console.WriteLine("Goodbye! ");
                                break;
                            case 1:
                                facade.Operation1(employee, CurrencyExchange.GrnToDollar,this);
                                break;
                            case 2:
                                facade.Operation1(employee, CurrencyExchange.GrnToEuro, this);
                                break;
                            case 3:
                                facade.Operation1(employee, CurrencyExchange.DollarToGrn, this);
                                break;
                            case 4:
                                facade.Operation1(employee, CurrencyExchange.EuroToGrn, this);
                                break;
                            case 5:
                                facade.Operation1(employee, CurrencyExchange.EuroToDollar, this);
                                break;
                            case 6:
                                facade.Operation1(employee, CurrencyExchange.DollarToEuro, this);
                                break;
                            default:
                                Console.WriteLine("It's imposible!");//це неможливо
                                return;
                        }
                        break;
                    #endregion
                    #region Case2
                    case 2:
                        employee.Question3();
                        punct2 = Convert.ToInt32(Console.ReadLine());
                        switch (punct2)
                        {
                            case 0:
                                Console.WriteLine("Goodbye! ");
                                break;
                            case 1:
                                facade.Operation2(0.15);
                                break;
                            case 2:
                                facade.Operation2(0.17);
                                break;
                            case 3:
                                facade.Operation2(0.20);
                                break;
                            default:
                                Console.WriteLine("It's imposible!");//це неможливо
                                return;
                        }
                        break;
                    #endregion
                    #region Case3
                    case 3:
                        employee.Question4();
                        punct3 = Convert.ToInt32(Console.ReadLine());
                        switch (punct3)
                        {
                            case 0:
                                Console.WriteLine("Goodbye! ");
                                break;
                            case 1:
                                CDialogs.AboutLoan();
                                break;
                            default:
                                Console.WriteLine("It's imposible!");//це неможливо
                                return;
                        }
                        break;
                    #endregion
                    #region Case4
                    case 4:
                        employee.Question5();
                        punct4 = Convert.ToInt32(Console.ReadLine());
                        switch (punct4)
                        {
                            case 0:
                                Console.WriteLine("Goodbye! ");
                                break;
                            case 1:
                                Console.WriteLine("Payment account: ");//Платіжний рахунок
                                double Pay = Convert.ToDouble(Console.ReadLine());
                                Console.WriteLine("How much do you want to top up the account?(Commission 1.5%) ");//На яку суму бажаєте поповнити рахунок?
                                double Babki = Convert.ToDouble(Console.ReadLine());
                                double Babki2 = ((Babki * 0.015) + Babki);
                                Console.WriteLine("You need to pay:   " + Babki2 + "grn");//Потрібно оплатити:
                                Console.WriteLine("Pay?(Yes - 1, NO - 2)");
                                int Pay1 = Convert.ToInt32(Console.ReadLine());
                                if (Pay1 == 1)
                                {

                                }
                                if (Pay1 == 2)
                                {
                                    Console.WriteLine($"Goodbye! ");
                                }

                                break;
                        }
                        break;
                    #endregion
                }
                Console.WriteLine("1. Continue , 2. To finish");
                if (Int32.Parse(Console.ReadLine()) == 1)
                {
                    //Програма продовжується
                }
                else
                {
                    bank.GetD(2);
                    check = false;
                }
            }
        }
        #endregion
    }
    class CConsultant : CPerson
    {
        public CConsultant(string name, int age) : base(name, age)
        {

        }

        internal CCustomer CCostumer
        {
            get => default;
            set
            {
            }
        }

        public void Consult()
        {
            Console.WriteLine("Good afternoon, how can I help?");//Доброго дня,чим можу допомогти?
            Console.WriteLine("0. Unfortunately, you won't be able to help me");//На жаль ви нічим мені не допоможете
            Console.WriteLine("1. Exchange money");//обміняти гроші
            Console.WriteLine("2. Invest money");//Вкласти гроші
            Console.WriteLine("3. Apply for a loan");//Оформити кредит
            Console.WriteLine("4. Pay the bill at the cash desk");//Оплатити рахунок в касі
        }
    }
    class CEmployee : CPerson
    {
        CComputer _computer;
        public string Report { get; set; }
        public CEmployee(string name, int age, CComputer computer) : base(name, age)
        {
            _computer = computer;
        }
        public double _grnAmount = 2147483647;//кількість гривень в касі
        public double _dollarAmount = 65535;//кількість доларів в касі
        public double _euroAmount = 1287;//кількість євро в касі
        public double Exchange(CurrencyExchange currencyOut, double customerStartMoney,CCustomer costumer)
        {
            double resultAmount = _computer.Exchange(currencyOut, customerStartMoney);
            switch (currencyOut)
            {
                case CurrencyExchange.GrnToDollar:
                    _dollarAmount -= resultAmount;
                    _grnAmount += customerStartMoney;
                    Report += $"Name:{costumer.Name} Start Money: {customerStartMoney} Result: {resultAmount} Date: {DateTime.Now.ToString("ddd, dd MMM yyy HH':'mm':'ss 'GMT'")}\n";
                    break;
                case CurrencyExchange.GrnToEuro:
                    _euroAmount -= resultAmount;
                    _grnAmount += customerStartMoney;
                    Report += $"Name:{costumer.Name} Start Money: {customerStartMoney} Result: {resultAmount} Date: {DateTime.Now.ToString("ddd, dd MMM yyy HH':'mm':'ss 'GMT'")}\n";
                    break;
                case CurrencyExchange.DollarToGrn:
                    _dollarAmount += customerStartMoney;
                    _grnAmount -= resultAmount;
                    Report += $"Name:{costumer.Name} Start Money: {customerStartMoney} Result: {resultAmount} Date: {DateTime.Now.ToString("ddd, dd MMM yyy HH':'mm':'ss 'GMT'")}\n";
                    break;
                case CurrencyExchange.EuroToGrn:
                    _euroAmount += resultAmount;
                    _grnAmount -= customerStartMoney;
                    Report += $"Name:{costumer.Name} Start Money: {customerStartMoney} Result: {resultAmount} Date: {DateTime.Now.ToString("ddd, dd MMM yyy HH':'mm':'ss 'GMT'")}\n";
                    break;
                case CurrencyExchange.EuroToDollar:
                    _euroAmount += resultAmount;
                    _dollarAmount -= customerStartMoney;
                    Report += $"Name:{costumer.Name} Start Money: {customerStartMoney} Result: {resultAmount} Date: {DateTime.Now.ToString("ddd, dd MMM yyy HH':'mm':'ss 'GMT'")}\n";
                    break;
                case CurrencyExchange.DollarToEuro:
                    _dollarAmount += customerStartMoney;
                    _euroAmount -= resultAmount;
                    Report += $"Name:{costumer.Name} Start Money: {customerStartMoney} Result: {resultAmount} Date: {DateTime.Now.ToString("ddd, dd MMM yyy HH':'mm':'ss 'GMT'")}\n";
                    break;
                default:
                    return 0;
            }
            return resultAmount;
        }
        public void Question1()
        {
            Console.WriteLine("Choose a punct:");//виберіть пункт
            Console.WriteLine("0. End of exchange");//завершення обміну
            Console.WriteLine("1. Exchange grn to dollar");//обміняти гривні на долари
            Console.WriteLine("2. Exchange grn to euro");//обміняти гривні на євро
            Console.WriteLine("3. Exchange dollar to grn");//обміняти долари на гривні
            Console.WriteLine("4. Exchange euro to grn");//обміняти євро на гривні
            Console.WriteLine("5. Exchange euro to dollar");//обміняти євро на долари
            Console.WriteLine("6. Exchange dollar to euro");//обміняти долари на євро
        }
        public void Question2(CurrencyExchange currencyOut)
        {
            Console.WriteLine("How many " + currencyOut + " you want to exchange?");//Скільки currencyOut Ви хочете обміняти
        }
        public void Question3()
        {
            Console.WriteLine("Good afternoon, for which term do you want to invest money, minimum deposit 5000 hryvnias");//Доброго дня на який термін ви хочете вкласти гроші, мінімальний депозит 5000 гривень
            Console.WriteLine("0. End of invest");//завершення вкладу
            Console.WriteLine("1. For half a year at 15% per annum ");//На пів року під 15 відсотків річних
            Console.WriteLine("2. For a year at 17% per annum ");//На рік під 17 відсотків річних
            Console.WriteLine("3. For 2 years at 20% per annum ");//На 2 роки під 20 відсотків річних
        }
        public void Question4()
        {
            Console.WriteLine("Good day, please write the data from the passport and the identification code");//Доброго дня, напишіть будь ласка дані з паспорту та індифікаційний код
            Console.WriteLine("0. Completion of the loan");//завершення взяття кредиту
            Console.WriteLine("1. We continue");//Продовжуємо
        }
        public void Question5()
        {
            Console.WriteLine("Good day, please dictate the number of the account that needs to be topped up:");//Доброго дня, продиктуйте будь-ласка номер рахунку який треба поповнити
            Console.WriteLine("0. Completion of the operation");//Завершення операції
            Console.WriteLine("1. We continue");//Продовжуємо
        }

        internal CComputer CComputer
        {
            get => default;
            set
            {
            }
        }

        internal CBank CBank
        {
            get => default;
            set
            {
            }
        }

        internal ReportBankSingleton ReportBankSingleton
        {
            get => default;
            set
            {
            }
        }
    }
    class CDialogs
    {
        public static void AboutLoan()
        {
            Console.WriteLine("Enter your initials(Huk Vladislav Vitaliyovuch):");//Введіть ваші ініціали
            string pib = Console.ReadLine();
            Console.WriteLine("Enter your date of birth(day):");//Введіть вашу дату народження
            double dateDay = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter your date of birth(month):");//Введіть вашу дату народження
            double dateMonth = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter your date of birth(Year):");//Введіть вашу дату народження
            double dateYear = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter your residential address(Rivne, vul.Soborna 33, kvartura 124):");//Вкажіть вашу адресу проживання
            string adress = Console.ReadLine();
            Console.WriteLine("Enter your phone number:");//Вкажіть ваш номер телефону
            int number = Convert.ToInt32(Console.ReadLine());
            if (dateDay < 13 && dateMonth == 11 && dateYear == 2004 || dateYear < 2004 || dateMonth < 11 && dateYear == 2004)
            {
                Console.WriteLine("Your details have been verified and we can provide you with a loan at 1.5% per month");//Ваші дані перевірено і ми можемо надати вам кредит під 1,5% на місяць
                Console.WriteLine("What amount do you want to take a loan for?(5000 - 100000 grn)");//На яку суму ви хочете взяти кредит?
                int kredit = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("For what term do you take a loan?( 10, 20, 36 month)");//На який термін ви берете кредит?
                int Termin = Convert.ToInt32(Console.ReadLine());
                if (kredit >= 5000 && kredit <= 100000)
                {
                    if (Termin == 10)
                    {
                        double Kredit1 = ((kredit * 0.015) + kredit) / 10;
                        Console.WriteLine("Payment for the loan per month is:   " + Kredit1 + "grn");
                    }

                    if (Termin == 20)
                    {
                        double Kredit2 = ((kredit * 0.015) + kredit) / 20;
                        Console.WriteLine("Payment for the loan per month is:   " + Kredit2 + "grn");
                    }

                    if (Termin == 36)
                    {
                        double Kredit3 = ((kredit * 0.015) + kredit) / 36;
                        Console.WriteLine("Payment for the loan per month is:   " + Kredit3 + "grn");
                    }
                }
                else
                {
                    Console.WriteLine("We cannot give you credit");//Ми не можемо надати вам кредит
                }

            }
            else
            {
                Console.WriteLine("We cannot give you credit");//Ми не можемо надати вам кредит
            }
        }
    }
    class CBank
    {
        public static int ProcentD { get; set; }
        static CBank()
        {
            ProcentD = 20;
        }
        public void GetD(int dirty)
        {
            ProcentD += dirty;
        }
        public void PrintProcent()
        {
            Console.WriteLine($"ProcentDirty: {ProcentD} ");
        }
    }
    class CClinerMan : CPerson
    {
        public int ClinerPower { get; set; }

        internal CVinnik CVinnik
        {
            get => default;
            set
            {
            }
        }

        internal CBank CBank
        {
            get => default;
            set
            {
            }
        }

        public CClinerMan(string name, int age, int clinerPower) : base(name, age)
        {
            ClinerPower = clinerPower;
        }

        public int Cline(int procentD, ref int Hp, int Power)
        {
            ReportBankSingleton.Report += $"Before Dirty: {procentD}\n";
            while (procentD - (Power + ClinerPower) >= 0)
            {
                procentD -= (Power + ClinerPower);
                if (Hp <= 0)
                {
                    ReportBankSingleton.Report += "Old vinik desroy\n";
                    ReportBankSingleton.Report += "We give new vinik\n";
                    Hp = 6;
                    ReportBankSingleton.Report += $"Dirty: {procentD}\n";
                }
                Hp -= Power;

                ReportBankSingleton.Report += $"The vinnik had some left over:   { Hp } hp\n";
            }
            ReportBankSingleton.Report += $"Cline end\n";
            ReportBankSingleton.Report += $"After Dirty: {procentD}\n";
            return procentD;


        }
    }
    class CComputer
    {
        double _dollarRateSell;//ціна продажу
        double _dollarRateBuy;//ціна купівлі
        double _euroRateSell;//ціна продажу
        double _euroRateBuy;//ціна купівлі
        public CComputer(double dollarRateSell = 41.6, double dollarRateBuy = 41.5, double euroRateSell = 40.9, double EuroRateBuy = 40.5)//конструктор з вказаними пераметрами за замовчуванням
        {
            _dollarRateSell = dollarRateSell;
            _dollarRateBuy = dollarRateBuy;
            _euroRateSell = euroRateSell;
            _euroRateBuy = euroRateSell;

        }
        public double Exchange(CurrencyExchange currencyOut, double customerStartMoney)
        {
            switch (currencyOut)
            {
                case CurrencyExchange.GrnToDollar:
                    return customerStartMoney / _dollarRateSell;
                case CurrencyExchange.GrnToEuro:
                    return customerStartMoney / _euroRateSell;
                case CurrencyExchange.DollarToGrn:
                    return customerStartMoney * _dollarRateBuy;
                case CurrencyExchange.EuroToGrn:
                    return customerStartMoney * _euroRateBuy;
                case CurrencyExchange.EuroToDollar:
                    return customerStartMoney * _euroRateBuy;
                case CurrencyExchange.DollarToEuro:
                    return customerStartMoney * _dollarRateBuy;
                default:
                    return 0;
            }
        }


    }
    class CVinnik
    {
        public int Hp { get; set; }
        public int Power { get; set; }
        public CVinnik(int Hp = 6, int Power = 2)
        {
            this.Hp = Hp;
            this.Power = Power;
        }
    }
}
