Get-ChildItem sqlfluff_dist\*.whl | % {
  tar -xf $_.FullName -C "$env:repos\python\Lib\site-packages"
}
