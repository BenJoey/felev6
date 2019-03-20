%option noyywrap c++ yylineno

%{
#include <iostream>
#include "Parserbase.h"

using namespace std;

%}

DIGIT   [0-9]
IDENTIFIER_START [a-zA-Z]
IDENT {IDENTIFIER_START}({IDENTIFIER_START}|{DIGIT}|_)*
WS  [ \t\n]

%%

program     return Parser::PROGRAM;
begin       return Parser::TBEGIN;
end         return Parser::TEND;
natural     return Parser::NATURAL;
boolean     return Parser::BOOLEAN;
true        return Parser::TRUE;
false       return Parser::FALSE;
div         return Parser::DIV;
mod         return Parser::MOD;
and         return Parser::AND;
or          return Parser::OR;
not         return Parser::NOT;
skip        return Parser::SKIP;
if          return Parser::IF;
then        return Parser::THEN;
else        return Parser::ELSE;
elseif      return Parser::ELSEIF;
endif       return Parser::ENDIF;
while       return Parser::WHILE;
do          return Parser::DO;
done        return Parser::DONE;
read        return Parser::READ;
write       return Parser::WRITE;

{DIGIT}+    return Parser::NATURAL_LITERAL;
{IDENT}     return Parser::INDENTIFIER;

"("         return Parser::LEFT_BRACKET;
")"         return Parser::RIGHT_BRACKET;
":="        return Parser::ASSIGN;
"-"         return Parser::MINUS;
"+"         return Parser::PLUS;
"*"         return Parser::ASTERIKS;
"<"         return Parser::LESS_THAN;
">"         return Parser::GREATER_THAN;
"="         return Parser::EQUALS;
";"         return Parser::SEMICOLON;

{WS}+       // szokozok - semmi teendo
#[^#]*#     //komment - nincs teendo

.           {
                std::cerr << "Line " << lineno() << ": Parse error: Unexpected character '" << YYText() << "'" << std::endl;
                exit(1);
            }

%%