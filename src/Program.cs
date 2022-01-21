
using CSharpIdCardCheck;

string? idCardNO;

begin:
Console.WriteLine("请输入身份证:");
idCardNO = Console.ReadLine();

if(string.IsNullOrEmpty(idCardNO))
    goto begin;

if(idCardNO.Length != 18)
{
    Console.WriteLine("身份证位数错误");
    goto begin;
}


CheckCodeTool cct = new CheckCodeTool();
bool check = new CheckCodeTool().CheckIdCard(idCardNO);

if(check)
    Console.WriteLine("校验成功");
else
    Console.WriteLine("校验失败");