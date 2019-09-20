parser grammar MacroParser;

@header {
	#pragma warning disable 3021
}

options { tokenVocab=MacroLexer; }


preprocess
		:	procLine+
		;

procLine
		:	macroDefine																						#defineLine
		|	.																								#notADefineLine
		;

macroDefine
		:	(DEFINE_DIRECTIVE | C_MAKE_DEFINE) defineType End
		;

defineType
		:   IDENTIFIER  LPAREN	spaces	(macroParam	spaces (COMMA spaces macroParam spaces)*)? RPAREN WS? macro_text?				#defineFunctionLikeDirective
		|  	IDENTIFIER	spaces macro_text?																							#defineObjectLikeDirective
		;

spaces
		:	(WS | BACKSLASH_NEWLINE)*
		;

macroParam
		:	IDENTIFIER spaces ELLIPSIS																				
		|	ELLIPSIS																						
		|	IDENTIFIER																						
		;

macro_text
		:	source_text+
		;

source_text
		:   sourceExpression																#macroSourceExpression
		|	COMMA																			#macroComma
		|	LPAREN																			#macroLParen
		|	RPAREN																			#macroRParen
		|	WS																				#macroWhitespace
		;

sourceExpression
		:   macroExpansion																	#macroExpansionToken
		|	concatenate																		#concatenation
		|	STRINGIFICATION	IDENTIFIER														#stringification
		|	primarySource																	#primaryTokens
		|	SIZEOF																			#sizeof
		|	LPAREN args? RPAREN																#arguments
		|	SEMICOLON																		#semicolon
		|	LINE_COMMENT																	#lineComment
		|	WS																				#whitespace
		;

args	:	mArg (multipleArg)*
		;

multipleArg
		:	WS? COMMA WS? mArg
		;

macroExpansion
		:	IDENTIFIER WS? LPAREN WS? (macArgs  WS?)? RPAREN
		;

macArgs	:	mArg ( WS? COMMA WS? mArg)*
		;

mArg	:	sourceExpression+
		|
		;

concatenate
		:	primarySource? ( WS? SHARPSHARP  WS? primarySource )+
		;

constant
		:   HEX_LITERAL
		|   OCTAL_LITERAL
		|   DECIMAL_LITERAL
		|	STRING_LITERAL
		|   FLOATING_POINT_LITERAL
		;

primarySource
		: 	SHARPSHARP WS?																	#PasteOperator
		|	VA_ARGS																			#variadicArguments
		|	STRING_OP																		#stringized
		|	IDENTIFIER																		#PrimaryIdentifier
		|	constant																		#PrimaryConstant
		|	CKEYWORD																		#PrimaryCKeyword
		|	COPERATOR																		#PrimaryCOperator
		|	BACKSLASH_NEWLINE																#PrimaryNewLine
		;