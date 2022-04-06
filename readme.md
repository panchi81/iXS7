# iX Developer Tag Export Siemens S7 DataBlock Generator

```mermaid
flowchart TD
    export(Export iX Tag List) --> read[Open and read the input file]

    read --> parse[Parse the input file, sort by Tag address in corresponding Data block]

    parse --> CheckDBs[ For each Data block, Generate list of missing entries in DB starting from 0]

    CheckDBs --> Calc[Calculate offset of missing entries]

    Calc --> Insert[Insert corresponding Datatype]

    Insert --> Merge[merge with the initial DB-entires list]
```
