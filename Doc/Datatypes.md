# iX Developer S7 ISO over TCP/IP Data types

## First table

iX Tag Format|S7 Tag Format|Range|Clarification|Example
-|-|-|-|-
DEFAULT|-|-32768 to 32767 ($-2^{15}$ to $2^{15}-1$)|Signed INT 16 - iX Specific|
BIT|BOOL|0 or 1||I0.1 Q0.1 M0.1 DB1.DBX0.1 V0.1
BOOL|BOOL|FALSE (0) or TRUE (1)
INT16|BYTE||Signed INT 16|IB2 QB2 MB2 DB1.DBB2 VB2
INT16|INT|$-32768$ ($-2^{15}$) to $32767$ ($2^{15}-1$)|Signed INT 16|IW2 QW2 MW2 DB1.DBW2 VW2
UINT16|UINT| $0$ to $65535$ ($2^{16}-1$)|Unsigned INT 16|IW2 QW2 MW2 DB1.DBW2 VW2
INT32|DINT|$-2^{31}$ to $2^{31}-1$|Signed INT 32|ID2 QD2 MW2 DB1.DBW2 VW2
UINT32|UDINT|$0$ to $4 294 967 295$ ($2^{32}-1$)|Unsigned INT 32
FLOAT|REAL|iX $±3.4 \times 10^{38}$, S7 IEEE754 $-3.402823 \times 10^{38}$ to $-1.175495 \times 10^-{38}$, $+1.175495 \times 10^-{38}$ to $3.402823 \times 10^{38}$
DOUBLE|LREAL|iX: $1.7 \times 10^{308}$, S7: 0 to $2^{64}-1$
STRING|STRING|0 to 254 Characters

## Second Table

Type|S7 designation|Bit width|Range of values
-|-|-|-
Long floating-point number (in accordance with IEEE-754)|LREAL|64|$-1.7976931348623157 \times 10^{308}$ to $-2.2250738585072014 \times 10^{-308}$, $0.0$, $2.2250738585072014 \times 10^{-308}$ to $1.7976931348623157 \times 10^{308}$

Type|iX Designation|Bit width|Range
-|-|-|-
Float with exponent|DOUBLE|64|$1.7 \times 10^{308}$

iX      S7      (S7 Container)

FLOAT   REAL    DW
INT32   DINT    DW
INT32   DINT    DW
UINT32  UDINT   DW
INT16   INT     W
INT16   INT     W
UINT16  UINT    W
UINT16  Word    W
INT16   Byte    B
INT16   Byte    B
INT16   Byte    B
INT16   Byte    B
INT16   Byte    B
INT16   Byte    B
BIT...  Bool    X

## Linkies

* [mySCADA - S7 Data Types](https://www.myscada.org/mydesigner-manual/?section=s7-data-types-2)