# download-hero-images.ps1
$heroes = @"
Spider-Man
Iron Man
Captain America
Hulk
Thor
Black Panther
Doctor Strange
Wolverine
Scarlet Witch
Deadpool
Captain Marvel
Black Widow
Storm
Jean Grey
She-Hulk
Wonder Woman
Superman
Batman
Supergirl
Batgirl
Zatanna
Hawkgirl
Catwoman
The Flash
Aquaman
Green Lantern
"@.Split("`n") | ForEach-Object { $_.Trim() }

$all = Invoke-RestMethod 'https://akabab.github.io/superhero-api/api/all.json'

$output = "D:\dev\HeroFinder\HeroFinder\HeroFinder\wwwroot\images"
New-Item -ItemType Directory -Path $output -Force | Out-Null

foreach ($h in $all) {
    if ($heroes -contains $h.name) {
        $url = $h.images.lg
        $safe = ($h.name -replace '\s+','-').ToLower()
        $ext = ".jpg"
        $out = Join-Path $output "$safe$ext"
        Write-Host "Downloading $($h.name)...$($out)"
        Invoke-WebRequest -Uri $url -OutFile $out
    }
}
