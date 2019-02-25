default: compile

compile:
	flex lexer.l
	g++ -o lexer lex.yy.cc main.cpp

clean:
	rm -f lex.yy.cc lexer