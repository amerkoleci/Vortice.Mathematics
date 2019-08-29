@echo off
@setlocal

dotnet restore src/Vortice.Mathematics.sln

dotnet pack -c Release src/Vortice.Mathematics/Vortice.Mathematics.csproj
dotnet pack -c Release src/Vortice.Mathematics.PackedVector/Vortice.Mathematics.PackedVector.csproj
