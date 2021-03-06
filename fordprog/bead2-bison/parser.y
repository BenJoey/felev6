%baseclass-preinclude <iostream>
%lsp-needed

%token PROGRAM TBEGIN SKIP TEND
%token NATURAL BOOLEAN
%token TRUE FALSE
%token NOT
%token IF THEN ELSE ENDIF ELSEIF
%token WHILE DO DONE
%token READ WRITE
%token LEFT_BRACKET RIGHT_BRACKET
%token ASSIGN
%token SEMICOLON
%token NATURAL_LITERAL
%token IDENTIFIER

%left AND OR
%left EQUALS
%left LESS_THAN GREATER_THAN
%left PLUS MINUS
%left ASTERIKS DIV MOD

%%

start: program
    {
        std::cout << "start -> program" << std::endl;
    }
;
    
program: header declarations body
    {
        std::cout << "program -> header declarations body" << std::endl;
    }
;
    
header: PROGRAM IDENTIFIER
    {
        std::cout << "header -> PROGRAM IDENTIFIER" << std::endl;
    }
;
    
declarations
    :
        {
            std::cout << "declarations -> \"\"" << std::endl;
        }
    | declaration declarations
        {
            std::cout << "declarations -> declaration declarations" << std::endl;
        }
    ;
    
declaration: type IDENTIFIER SEMICOLON
    {
        std::cout << "declaration -> type IDENTIFIER SEMICOLON" << std::endl;
    }
;
    
type
    : NATURAL
        {
            std::cout << "type -> NATURAL" << std::endl;
        }
    | BOOLEAN
        {
            std::cout << "type -> BOOLEAN" << std::endl;
        }
    ;

body: TBEGIN statements TEND
    {
        std::cout << "body -> TBEGIN statements END" << std::endl;
    }
;
    
statements
    : statement
        {
            std::cout << "statements -> statement" << std::endl;
        }
    | statement statements
        {
            std::cout << "statements -> statement statements" << std::endl;
        }
    ;

statement
    : skip
        {
            std::cout << "statement -> skip" << std::endl;
        }
    | assignment
        {
            std::cout << "statement -> assignment" << std::endl;
        }
    | write
        {
            std::cout << "statement -> write" << std::endl;
        }
    | read
        {
            std::cout << "statement -> read" << std::endl;
        }
    | while_loop
        {
            std::cout << "statement -> while_loop" << std::endl;
        }
    | conditional
        {
            std::cout << "statement -> conditional" << std::endl;
        }
    ;

skip: SKIP SEMICOLON
    {
        std::cout << "skip -> SKIP SEMICOLON" << std::endl;
    }
;
    
assignment: IDENTIFIER ASSIGN expr SEMICOLON
    {
        std::cout << "assignment -> IDENTIFIER ASSIGN expr" << std::endl;
    }
;

write: WRITE LEFT_BRACKET expr RIGHT_BRACKET SEMICOLON
    {
        std::cout << "write -> WRITE LEFT_BRACKET expr RIGHT_BRACKET SEMICOLON" << std::endl;
    }
;

read: READ LEFT_BRACKET IDENTIFIER RIGHT_BRACKET SEMICOLON
    {
        std::cout << "read -> READ LEFT_BRACKET IDENTIFIER RIGHT_BRACKET SEMICOLON" << std::endl;
    }
;

while_loop: WHILE expr DO statements DONE
    {
        std::cout << "while_loop -> WHILE expr DO statements DONE" << std::endl;
    }
;
    
conditional
    : IF expr THEN statements elseifs ENDIF
        {
            std::cout << "conditional -> IF expr THEN statements elseifs ENDIF" << std::endl;
        }
    | IF expr THEN statements elseifs ELSE statements ENDIF
        {
            std::cout << "conditional -> IF expr THEN statements elseifs ELSE statements ENDIF" << std::endl;
        }
    ;
    
elseifs
    :
       // Empty
    | ELSEIF expr THEN statements elseifs
        {
            std::cout << "elseifs -> ELSEIF expr THEN statements elseifs" << std::endl;
        }
    ;

expr
    : TRUE
        {
            std::cout << "expr -> TRUE" << std::endl;
        }
    | FALSE
        {
            std::cout << "expr -> FALSE" << std::endl;
        }
    | NATURAL_LITERAL
        {
            std::cout << "expr -> NATURAL_LITERAL" << std::endl;
        }
    | IDENTIFIER
        {
            std::cout << "expr -> IDENTIFIER" << std::endl;
        }
    | expr EQUALS expr
        {
            std::cout << "expr -> expr EQUALS expr" << std::endl;
        }
    | LEFT_BRACKET expr RIGHT_BRACKET
        {
            std::cout << "expr -> LEFT_BRACKET expr RIGHT_BRACKET" << std::endl;
        }
    | NOT expr
        {
            std::cout << "expr -> NOT expr" << std::endl;
        }
    | expr AND expr
        {
            std::cout << "expr -> expr AND expr" << std::endl;
        }
    | expr OR expr
        {
            std::cout << "expr -> expr OR expr" << std::endl;
        }
    | expr LESS_THAN expr
        {
            std::cout << "expr -> expr LESS_THAN expr" << std::endl;
        }
    | expr GREATER_THAN expr
        {
            std::cout << "expr -> expr GREATER_THAN expr" << std::endl;
        }
    | expr PLUS expr
        {
            std::cout << "expr -> expr PLUS expr" << std::endl;
        }
    | expr MINUS expr
        {
            std::cout << "expr -> expr MINUS expr" << std::endl;
        }
    | expr ASTERIKS expr
        {
            std::cout << "expr -> expr ASTERIKS expr" << std::endl;
        }
    | expr MOD expr
        {
            std::cout << "expr -> expr MOD expr" << std::endl;
        }
    | expr DIV expr
        {
            std::cout << "expr -> expr DIV expr" << std::endl;
        }
    ;
