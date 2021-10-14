from string import digits, ascii_letters

sorted_tags = {
    "DB101": ["DB101.DBW2", "DB101.DBD4", "DB101.DBD12", "DB101.DBD16", "DB101.DBD96"],
    "DB104": [
        "DB104.DBD0",
        "DB104.DBD4",
        "DB104.DBD8",
        "DB104.DBD12",
        "DB104.DBD16",
        "DB104.DBD20",
    ],
}

entries = [i for _, addresses in sorted_tags.items() for i in addresses]

entries = [
    "DB101.DBW2",
    "DB101.DBD4",
    "DB101.DBD12",
    "DB101.DBD16",
    "DB101.DBD96",
    "DB104.DBD0",
    "DB104.DBD4",
    "DB104.DBD8",
    "DB104.DBD12",
    "DB104.DBD16",
    "DB104.DBD20",
]

missing = []

siemens_s7_DB_datatypes = {
    "DBD": {"type": "Float", "syntax": "Real", "offset": 4},
    "DBW": {"type": "INT16_W", "syntax": "Word", "offset": 2},
    "DBB": {"type": "INT16_B", "syntax": "Byte", "offset": 1},
    "DBX": {"type": "BIT", "syntax": "Bool", "offset": 0.1},
}
