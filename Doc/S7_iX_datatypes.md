# S7 / iX Tag Format

S7 Data Type|Description|Range|iX Tag Format|iX Data Type|Range exp.
-|-|-|-|-|-
BOOL|Bit|FALSE (0) or TRUE (1)|Bit|BIT|0 to $2^{0}$
BYTE  USINT|8-bit Unsigned Integer|0 to 255|Signed 16-bit|INT16|0 to $2^{8}-1$
CHAR  SINT|8-bit Unsigned Integer|-128 to 127|Signed 16-bit|INT16|$-2^{7}$ to $2^{7}-1$
WORD  UINT|16-bit Unsigned Integer|0 to 65535|Unsigned 16-bit|UINT16|0 to $2^{16}-1$
INT|16-bit Signed Integer|-32768 to 32767|Signed 16-bit|INT16|$-2^{15}$ to $2^{15}-1$
DWORD  UDINT|32-bit Unsigned Integer|0 to 4294967295|Unsigned 32-bit|UINT32|0 to $2^{64}-1$
DINT|32-bit Signed Integer|-2147483648 to 2147483647|Signed 32-bit|INT32|$-2^{31}$ to $2^{31}-1$
REAL|IEEE Float|$±3.4 \times 10^{38}$|Float 32-bit|FLOAT|$±3.4 \times 10^{38}$

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