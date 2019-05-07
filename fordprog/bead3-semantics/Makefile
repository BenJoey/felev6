default: compile

compile:
	flex lexer.l
	bisonc++ parser.y
	g++ -obead3 main.cc parse.cc lex.yy.cc

clean:
	rm -f lex.yy.cc lexer
