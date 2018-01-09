CSC=xbuild
DOXYGEN=doxygen

all: tim doc
    
tim:
	$(CSC) /p:Configuration=Release tim_dodge.sln
	
run:
	cd bin/Release && mono tim_dodge.exe

doc:
	$(DOXYGEN) doxygen_cfg

clean:
	rm -fr bin obj Documentation tim_tests/bin tim_tests/obj

test:
	cd tim_tests/bin/Release && (mono tim_tests.exe ; exit `cat tests_output`)
