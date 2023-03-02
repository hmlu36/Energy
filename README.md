7CVnyiwjnZS9

測試機
114.33.230.217
RDPUSER
p@0919785876


L2TP
IP：218.161.27.200
使用者帳號：ronnie
使用者密碼：fnV82Az$W8C88Se
金鑰：n6tzwXQRxm!hbE8

遠端桌面IP：210.65.88.31
帳號：administrator
密碼：iZ7W9Up5fd23TS6u7ih2v

Server=localhost;Database=master;Trusted_Connection=True;

dotnet ef dbcontext scaffold "Server=localhost;Database=vvspor-energy_v2;Trusted_Connection=True;Trusted_Connection=True;TrustServerCertificate=true;MultipleActiveResultSets=true" "Microsoft.EntityFrameworkCore.SqlServer" -o ./Models -c EnergyDbContext -f


dotnet user-secrets init
dotnet user-secrets set DbConnectonString "Server=localhost;Database=vvspor-energy_v2;Trusted_Connection=True;Trusted_Connection=True;TrustServerCertificate=true;MultipleActiveResultSets=true" 
dotnet ef dbcontext scaffold Name=DbConnectonString Microsoft.EntityFrameworkCore.SqlServer -o ./Models/DB -c EnergyDbContext -f