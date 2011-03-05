if ($args.length -eq 0)
{
  "Please provide a name"
  exit
}
$name = $args[0]
makecert.exe -sv tmp.pvk -n "CN=MoKhan.ca." tmp.cer
pvk2pfx.exe -pvk tmp.pvk -spc tmp.cer -pfx tmp.pfx
mv ./tmp.pfx ./$name.pfx
rm ./tmp.pvk
rm ./tmp.cer
