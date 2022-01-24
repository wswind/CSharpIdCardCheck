namespace CSharpIdCardCheck
{
    public class IdCardNOCheckTool
    {
        public static int CharToInt(char c)
        {
            if (c == 'X')
                return 10;
            else
            {
                if (c >= '0' && c <= '9')
                    return int.Parse(c.ToString());
                else
                    throw new ArgumentOutOfRangeException(nameof(c));
            }
        }

        public char GetCheckCode(int n)
        {
            if (n > 10 || n < 0)
                throw new ArgumentOutOfRangeException(nameof(n));
            return IntCheckCodeMap[n];
        }

        protected Dictionary<int, char> IntCheckCodeMap { get; set; } = new Dictionary<int, char>
        {
           {0,'1'}, {1,'0'},{2,'X'},{3,'9'},{4,'8'},{5,'7'},{6,'6'},{7,'5'},{8,'4'},{9,'3'},{10,'2'}
        };

        public bool CheckIdCard(string idCard)
        {
            int count = idCard.Length;

            if (count != 18)
                throw new ArgumentException("应传递十八位身份", nameof(idCard));

            string idCardSubString = idCard.Substring(0, 17);
            string idCardCheckCode = idCard.Substring(17, 1);

            string checkNO = GetIdCardCheckNO(idCardSubString).ToString();

            if (checkNO == idCardCheckCode)
                return true;
            else
                return false;
        }

        public char GetIdCardCheckNO(string idCardWithoutCheckCode)
        {
            int count = idCardWithoutCheckCode.Length;
            if (count != 17)
                throw new ArgumentException("应传递身份证前十七位", nameof(idCardWithoutCheckCode));

            int sumNum = 0;
            for (int i = 0; i < count; i++)
            {
                var c = idCardWithoutCheckCode[i];
                int n = CharToInt(c);
                var pow = ((int)Math.Pow(2, 17 - i)) % 11;
                sumNum += n * pow;
            }

            var check_code = GetCheckCode(sumNum % 11);
            return check_code;
        }
    }

}