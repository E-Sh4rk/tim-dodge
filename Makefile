CSC=xbuild
DOXYGEN=./Tools/doxygen-1.8.13

all: tim doc
    
tim:
	$(CSC) /p:Configuration=Release tim_dodge.sln
	
run:
	cd bin/Release && mono tim_dodge.exe

doc:
	$(DOXYGEN)/bin/doxygen doxygen_cfg

clean:
	rm -fr bin obj Documentation