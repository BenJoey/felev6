%option noyywrap c++ yylineno

%{
#include <iostream>
#include <cstdlib>
%}

DIGIT   [0-9]
IDENTIFIER_START [a-zA-Z]
IDENT {IDENTIFIER_START}({IDENTIFIER_START}|{DIGIT}|_)*
WS  [ \t\n]

%%

program     std::cout << "kulcsszo: " << YYText() << std::endl;
begin       std::cout << "kulcsszo: " << YYText() << std::endl;
end         std::cout << "kulcsszo: " << YYText() << std::endl;
natural     std::cout << "kulcsszo: " << YYText() << std::endl;
boolean     std::cout << "kulcsszo: " << YYText() << std::endl;
true        std::cout << "tipus: boolean: " << YYText() << std::endl;
false       std::cout << "tipus: boolean: " << YYText() << std::endl;
div         std::cout << "operator: " << YYText() << std::endl;
mod         std::cout << "operator: " << YYText() << std::endl;
and         std::cout << "operator: " << YYText() << std::endl;
or          std::cout << "operator: " << YYText() << std::endl;
not         std::cout << "operator: " << YYText() << std::endl;
skip        std::cout << "kulcsszo: " << YYText() << std::endl;
if          std::cout << "kulcsszo: " << YYText() << std::endl;
then        std::cout << "kulcsszo: " << YYText() << std::endl;
else        std::cout << "kulcsszo: " << YYText() << std::endl;
elseif      std::cout << "kulcsszo: " << YYText() << std::endl;
endif       std::cout << "kulcsszo: " << YYText() << std::endl;
while       std::cout << "kulcsszo: " << YYText() << std::endl;
do          std::cout << "kulcsszo: " << YYText() << std::endl;
done        std::cout << "kulcsszo: " << YYText() << std::endl;
read        std::cout << "kulcsszo: " << YYText() << std::endl;
write       std::cout << "kulcsszo: " << YYText() << std::endl;

{DIGIT}+    std::cout << "tipus: number: " << YYText() << std::endl;
{IDENT}     std::cout << "valtozo: " << YYText() << std::endl; 

"("         std::cout << "bracket: " << YYText() << std::endl;
")"         std::cout << "bracket: " << YYText() << std::endl;
":="        std::cout << "operator: " << YYText() << std::endl;
"-"         std::cout << "operator: " << YYText() << std::endl;
"+"         std::cout << "operator: " << YYText() << std::endl;
"*"         std::cout << "operator: " << YYText() << std::endl;
"."         std::cout << "operator: " << YYText() << std::endl;
"<"         std::cout << "operator: " << YYText() << std::endl;
">"         std::cout << "operator: " << YYText() << std::endl;
"="         std::cout << "operator: " << YYText() << std::endl;
";"         std::cout << "separator: " << YYText() << std::endl;

{WS}+       // szokozok - semmi teendo
#[^#]*#     //komment - nincs teendo

.           {
                std::cerr << "Line " << lineno() << ": Parse error: Unexpected character '" << YYText() << "'" << std::endl;
                exit(1);
            }

%%