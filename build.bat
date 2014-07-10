@echo Off
set config=%1
if "%config%" == "" (
   set config=Release
)

set version=
if not "%PackageVersion%" == "" (
   set version=-Version %PackageVersion%
)

REM Build
msbuild AW.Model.RWX.sln /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false

REM Package
mkdir Build
cmd /c %nuget% pack "Bloyteg.AW.Model.RWX\Bloyteg.AW.Model.RWX.fsproj" -symbols -o Build -p Configuration=%config% %version% -IncludeReferencedProjects