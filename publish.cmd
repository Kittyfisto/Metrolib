@setlocal
@echo off

echo Files in directory:
dir . /a-d

echo Looking for nuget package...
FOR %%F IN (Metrolib.*.nupkg) DO (
 set NUGET_PACKAGE=%%F
)

if not %NUGET_PACKAGE%=="" (
	if "%APPVEYOR_REPO_BRANCH%"=="master" (
		echo Publishing nuget package...
		@nuget push -Source https://api.nuget.org/v3/index.json -ApiKey %NUGET_API_KEY% %NUGET_PACKAGE%
	) else (
		echo Not publishing from this branch...
	)
) else (
	echo Did not find anything to publish!
)
