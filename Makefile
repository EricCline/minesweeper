deps:
	nuget restore

build Minesweeper/obj/Debug/Minesweeper.exe Minesweeper.Tests/obj/Debug/Minesweeper.Tests.dll:
	xbuild /nologo /v:q /property:GenerateFullPaths=true Minesweeper.sln

run: Minesweeper/obj/Debug/Minesweeper.exe
	open Minesweeper/bin/Debug/Minesweeper.app

test: Minesweeper.Tests/obj/Debug/Minesweeper.Tests.dll 
	nunit-console -nologo Minesweeper.Tests/Minesweeper.Tests.csproj

clean:
	rm -rf Minesweeper/obj Minesweeper/bin Minesweeper.Tests/obj Minesweeper.Tests/bin

.PHONY: clean run build test deps
