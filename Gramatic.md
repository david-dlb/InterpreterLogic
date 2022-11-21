Gramatica:
- comp: expr | (LESS | MORE | EQUAL) expr
- Logic: comp | AND | OR | comp
- IF: IF LPAREN comp RPAREN BEGIN COMPOUND END 
- expr : term ((PLUS | MINUS) term)*

- term : factor ((MUL | DIV) factor)*

- factor : PLUS factor
       | MINUS factor
       | INTEGER factor
       | L_PARENT expr R_PARENT