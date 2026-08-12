''Module BOM
'' Part Number
'iProperties.Value("Custom", "PART NO.") = 1

'' Quantity (default = 1)
'iProperties.Value("Custom", "PART QTY.") = 1

'' Description
'iProperties.Value("Custom", "DESCRIPTION") = "TANK SHELL PLATE"

'' Size (example – parameter driven)
'iProperties.Value("Custom", "SIZE") = "PL 3787" & " x " & Parameter("LENGTH") & " x " & Parameter("THK") & " THK"

'' Material (from physical)
'iProperties.Value("Custom", "MATERIAL") = Parameter("MATERIAL")

'iProperties.Material = MATERIAL

'' Weight in kg
'iProperties.Value("Custom", "WEIGHT (kg)") = 325

'' Remarks
'iProperties.Value("Custom", "REMARKS") = ""

''==========================
'' PART LIST – CUSTOM PROPS
''==========================

'' Part Number
'iProperties.Value("Custom", "PART NO.") = 2

'' Quantity (default = 1)
'iProperties.Value("Custom", "PART QTY") = 1

'' Description
'iProperties.Value("Custom", "DESCRIPTION") = "2:1 ELLIPSOIDAL HEAD"

'' Size (example – parameter driven)
'iProperties.Value("Custom", "SIZE") = "PL ID " & Parameter("ID") & " x " & Parameter("THK") & " THK"

'' Material (from physical)
'iProperties.Value("Custom", "MATERIAL") = Parameter("MATERIAL")

'iProperties.Material = MATERIAL

'' Weight in kg
'iProperties.Value("Custom", "WEIGHT (kg)") = 94

'' Remarks
'iProperties.Value("Custom", "REMARKS") = ""

'    ' Part Number
'iProperties.Value("Custom", "PART NO.") = 3

'' Quantity (default = 1)
'iProperties.Value("Custom", "PART QTY") = 1

'' Description
'iProperties.Value("Custom", "DESCRIPTION") = "TANK TOP COVER PLATE"

'' Size (example – parameter driven)
'iProperties.Value("Custom", "SIZE") = "PL ø"  & Parameter("OD") & " X " & Parameter("THK") & " THK"

'' Material (from physical)
'iProperties.Value("Custom", "MATERIAL") = Parameter("MATERIAL")

'iProperties.Material = MATERIAL

'' Weight in kg
'iProperties.Value("Custom", "WEIGHT (kg)") = 81

'' Remarks
'iProperties.Value("Custom", "REMARKS") = ""

'' Part Number
'iProperties.Value("Custom", "PART NO.") = 4

'' Quantity (default = 1)
'iProperties.Value("Custom", "PART QTY") = 1

'' Description
'iProperties.Value("Custom", "DESCRIPTION") = "CURB ANGLE"

'' Size (example – parameter driven)
'iProperties.Value("Custom", "SIZE") = "L " & Parameter("LENGTH") & " x " & Parameter("LENGTH") & " x " & Parameter("THK") & " - 4147 LG"

'' Material (from physical)
'iProperties.Value("Custom", "MATERIAL") = Parameter("MATERIAL")

'iProperties.Material = MATERIAL

''Sync to Custom property
'iProperties.Value("Custom", "MATERIAL") = iProperties.Material

'' Weight in kg
'iProperties.Value("Custom", "WEIGHT (kg)") = 23

'' Remarks
'iProperties.Value("Custom", "REMARKS") = ""


'' Part Number
'iProperties.Value("Custom", "PART NO.") = 5

'' Quantity (default = 1)
'iProperties.Value("Custom", "PART QTY") = 4

'' Description
'iProperties.Value("Custom", "DESCRIPTION") = "PAD FOR LIFTING LUG"

'' Size (example – parameter driven)
'iProperties.Value("Custom", "SIZE") = "PL " & Parameter("LL_HEIGHT") & "x" & Parameter("LL_LENGTH") & "x" & Parameter("LL_THK") & "THK"

'' Material (from physical)
'iProperties.Value("Custom", "MATERIAL") = Parameter("MATERIAL")

'iProperties.Material = MATERIAL

'' Weight in kg
'iProperties.Value("Custom", "WEIGHT (kg)") = 8

'' Remarks
'iProperties.Value("Custom", "REMARKS") = ""


'' Part Number
'iProperties.Value("Custom", "PART NO.") = 6

'' Quantity (default = 1)
'iProperties.Value("Custom", "PART QTY") = 4

'' Description
'iProperties.Value("Custom", "DESCRIPTION") = "PLATE FOR LIFTING LUG"

'' Size (example – parameter driven)
'iProperties.Value("Custom", "SIZE") = "PL " & Parameter("d9") & "x" & Parameter("HEIGHT") & "x" & Parameter("THK") & "THK"

'' Material (from physical)
'iProperties.Value("Custom", "MATERIAL") = Parameter("MATERIAL")

'iProperties.Material = MATERIAL

'' Weight in kg
'iProperties.Value("Custom", "WEIGHT (kg)") = 16

'' Remarks
'iProperties.Value("Custom", "REMARKS") = ""


'' Part Number
'iProperties.Value("Custom", "PART NO.") = 7

'' Quantity (default = 1)
'iProperties.Value("Custom", "PART QTY") = 4

'' Description
'iProperties.Value("Custom", "DESCRIPTION") = "LEG SUPPORT"

'' Size (example – parameter driven)
'iProperties.Value("Custom", "SIZE") = "L " & Parameter("b") & "x" & Parameter("b") & "x" & Parameter("THK") & "THK"

'' Material (from physical)
'iProperties.Value("Custom", "MATERIAL") = Parameter("MATERIAL")

'iProperties.Material = MATERIAL

'' Weight in kg
'iProperties.Value("Custom", "WEIGHT (kg)") = 145

'' Remarks
'iProperties.Value("Custom", "REMARKS") = ""

'' Part Number
'iProperties.Value("Custom", "PART NO.") = 8

'' Quantity (default = 1)
'iProperties.Value("Custom", "PART QTY") = 4

'' Description
'iProperties.Value("Custom", "DESCRIPTION") = "PAD PLATE FOR TANK SUPPORT LEG"

'' Size (example – parameter driven)
'iProperties.Value("Custom", "SIZE") = "PL " & Parameter("TSL_HEIGHT") & "x" & Parameter("TSL_LENGTH") & "x" & Parameter("TSL_THK") & "THK"

'' Material (from physical)
'iProperties.Value("Custom", "MATERIAL") = Parameter("MATERIAL")

'iProperties.Material = MATERIAL

'' Weight in kg
'iProperties.Value("Custom", "WEIGHT (kg)") = 18

'' Remarks
'iProperties.Value("Custom", "REMARKS") = ""


'' Part Number
'iProperties.Value("Custom", "PART NO.") = 9

'' Quantity (default = 1)
'iProperties.Value("Custom", "PART QTY") = 4

'' Description
'iProperties.Value("Custom", "DESCRIPTION") = "CLOSER PLATE FOR TANK SUPPORT LEG"

'' Size (example – parameter driven)
'iProperties.Value("Custom", "SIZE") = "PL " & Parameter("LENGTH") & "x" & Parameter("HEIGHT") & "x" & Parameter("THK") & "THK"

'' Material (from physical)
'iProperties.Value("Custom", "MATERIAL") = Parameter("MATERIAL")

'iProperties.Material = MATERIAL

'' Weight in kg
'iProperties.Value("Custom", "WEIGHT (kg)") = 7

'' Remarks
'iProperties.Value("Custom", "REMARKS") = ""


'' Part Number
'iProperties.Value("Custom", "PART NO.") = 10

'' Quantity (default = 1)
'iProperties.Value("Custom", "PART QTY") = 4

'' Description
'iProperties.Value("Custom", "DESCRIPTION") = "BASE PLATE FOR TANK SUPPORT LEG"

'' Size (example – parameter driven)
'iProperties.Value("Custom", "SIZE") = "PL " & Parameter("LENGTH") & "x" & Parameter("HEIGHT") & "x" & Parameter("THK") & "THK"

'' Material (from physical)
'iProperties.Value("Custom", "MATERIAL") = Parameter("MATERIAL")

'iProperties.Material = MATERIAL

'' Weight in kg
'iProperties.Value("Custom", "WEIGHT (kg)") = 18

'' Remarks
'iProperties.Value("Custom", "REMARKS") = ""


'' Part Number
'iProperties.Value("Custom", "PART NO.") = 11

'' Quantity (default = 1)
'iProperties.Value("Custom", "PART QTY") = 1

'' Description
'iProperties.Value("Custom", "DESCRIPTION") = "PLATE FOR TOP COVER STIFFENER"

'' Size (example – parameter driven)
'iProperties.Value("Custom", "SIZE") = "PL 7200 x 80 x " & Parameter("THK") & "THK"

'' Material (from physical)
'iProperties.Value("Custom", "MATERIAL") = Parameter("MATERIAL")

'iProperties.Material = MATERIAL

'' Weight in kg
'iProperties.Value("Custom", "WEIGHT (kg)") = 28

'' Remarks
'iProperties.Value("Custom", "REMARKS") = ""


'' Part Number
'iProperties.Value("Custom", "PART NO.") = 12

'' Quantity (default = 1)
'iProperties.Value("Custom", "PART QTY") = 4

'' Description
'iProperties.Value("Custom", "DESCRIPTION") = "BOTTOM CLOSER PLATE FOR TANK LEG"

'' Size (example – parameter driven)
'iProperties.Value("Custom", "SIZE") = "PL " & Parameter("HEIGHT") & "x" & Parameter("LENGTH") & "x" & Parameter("THK") & "THK"

'' Material (from physical)
'iProperties.Value("Custom", "MATERIAL") = Parameter("MATERIAL")

'iProperties.Material = MATERIAL

'' Weight in kg
'iProperties.Value("Custom", "WEIGHT (kg)") = 3

'' Remarks
'iProperties.Value("Custom", "REMARKS") = ""


'' Part Number
'iProperties.Value("Custom", "PART NO.") = 14

'' Quantity (default = 1)
'iProperties.Value("Custom", "PART QTY") = 2

'' Description
'iProperties.Value("Custom", "DESCRIPTION") = "GROUNDING LUG"

'' Size (example – parameter driven)
'iProperties.Value("Custom", "SIZE") = "PL " &  Parameter("LENGTH") & "x" & Parameter("HEIGHT") & "x" & Parameter("THK") & "THK"

'' Material (from physical)
'iProperties.Value("Custom", "MATERIAL") = Parameter("MATERIAL")

'iProperties.Material = MATERIAL

'' Weight in kg
'iProperties.Value("Custom", "WEIGHT (kg)") = 1

'' Remarks
'iProperties.Value("Custom", "REMARKS") = ""

'' Part Number
'iProperties.Value("Custom", "PART NO.") = 15

'' Quantity (default = 1)
'iProperties.Value("Custom", "PART QTY") = 1

'' Description
'iProperties.Value("Custom", "DESCRIPTION") = "COVER PLATE FOR MANWAY"

'' Size (example – parameter driven)
'iProperties.Value("Custom", "SIZE") = "PL ø" &  Parameter("OD") & "x" & Parameter("THK") & "THK"

'' Material (from physical)
'iProperties.Value("Custom", "MATERIAL") = Parameter("MATERIAL")

'iProperties.Material = MATERIAL

'' Weight in kg
'iProperties.Value("Custom", "WEIGHT (kg)") = 70

'' Remarks
'iProperties.Value("Custom", "REMARKS") = ""


'' Part Number
'iProperties.Value("Custom", "PART NO.") = 16

'' Quantity (default = 1)
'iProperties.Value("Custom", "PART QTY") = 1

'' Description
'iProperties.Value("Custom", "DESCRIPTION") = "MANWAY FLANGE"

'' Size (example – parameter driven)
'iProperties.Value("Custom", "SIZE") = "PL OD " &  Parameter("OD") & "x ID " & Parameter("ID_") & "x" & Parameter("THK") & "THK"

'' Material (from physical)
'iProperties.Value("Custom", "MATERIAL") = Parameter("MATERIAL")

'iProperties.Material = MATERIAL

'' Weight in kg
'iProperties.Value("Custom", "WEIGHT (kg)") = 31

'' Remarks
'iProperties.Value("Custom", "REMARKS") = ""

'' Part Number
'iProperties.Value("Custom", "PART NO.") = 17

'' Quantity (default = 1)
'iProperties.Value("Custom", "PART QTY") = 1

'' Description
'iProperties.Value("Custom", "DESCRIPTION") = "PLATE FOR MANWAY NECK"

'' Size (example – parameter driven)
'iProperties.Value("Custom", "SIZE") = "PL 1941 x 291 x " &  Parameter("THK") & "THK"

'' Material (from physical)
'iProperties.Value("Custom", "MATERIAL") = Parameter("MATERIAL")

'iProperties.Material = MATERIAL

'' Weight in kg
'iProperties.Value("Custom", "WEIGHT (kg)") = 36

'' Remarks
'iProperties.Value("Custom", "REMARKS") = ""

'' Part Number
'iProperties.Value("Custom", "PART NO.") = 18

'' Quantity (default = 1)
'iProperties.Value("Custom", "PART QTY") = 1

'' Description
'iProperties.Value("Custom", "DESCRIPTION") = "PAD PLATE FOR MANWAY"

'' Size (example – parameter driven)
'iProperties.Value("Custom", "SIZE") = "PL OD 942 x ID " & Parameter("PAD_ID") & " x " & Parameter("THK") & "THK"

'' Material (from physical)
'iProperties.Value("Custom", "MATERIAL") = Parameter("MATERIAL")

'iProperties.Material = MATERIAL

'' Weight in kg
'iProperties.Value("Custom", "WEIGHT (kg)") = 19

'' Remarks
'iProperties.Value("Custom", "REMARKS") = ""


'' Part Number
'iProperties.Value("Custom", "PART NO.") = 20

'' Quantity (default = 1)
'iProperties.Value("Custom", "PART QTY") = 1

'' Description
'iProperties.Value("Custom", "DESCRIPTION") = "GASKET FOR MANWAY"

'' Size (example – parameter driven)
'iProperties.Value("Custom", "SIZE") = "PL OD " & Parameter("OD") & " x ID " &  Parameter("ID") & " x " & Parameter("THK") & "THK"

'' Material (from physical)
'iProperties.Value("Custom", "MATERIAL") = Parameter("MATERIAL")

'iProperties.Material = MATERIAL

'' Weight in kg
'iProperties.Value("Custom", "WEIGHT (kg)") = 3

'' Remarks
'iProperties.Value("Custom", "REMARKS") = ""


'    ' Part Number
'iProperties.Value("Custom", "PART NO.") = 21

'' Quantity (default = 1)
'iProperties.Value("Custom", "PART QTY") = 1

'' Description
'iProperties.Value("Custom", "DESCRIPTION") = "MANWAY EYE BOLT ROD"

'' Size (example – parameter driven)
'iProperties.Value("Custom", "SIZE") = "EYE BOLT WITH 2 HEX HEAVY NUTS - M" & Parameter("ROD_DIA") & " x " &  Parameter("ROD_LENGTH") & " LG"

'' Material (from physical)
'iProperties.Value("Custom", "MATERIAL") = Parameter("MATERIAL")

'iProperties.Material = MATERIAL

'' Weight in kg
'iProperties.Value("Custom", "WEIGHT (kg)") = 1

'' Remarks
'iProperties.Value("Custom", "REMARKS") = ""


'' Part Number
'iProperties.Value("Custom", "PART NO.") = 22

'' Quantity (default = 1)
'iProperties.Value("Custom", "PART QTY") = 1

'' Description
'iProperties.Value("Custom", "DESCRIPTION") = "MANWAY TO DAVIT CONNECTING PLATE"

'' Size (example – parameter driven)
'iProperties.Value("Custom", "SIZE") = "PL " & Parameter("LENGTH") & " x " &  Parameter("WIDTH") & " x " &  Parameter("THK") & "THK"

'' Material (from physical)
'iProperties.Value("Custom", "MATERIAL") = Parameter("MATERIAL")

'iProperties.Material = MATERIAL

'' Weight in kg
'iProperties.Value("Custom", "WEIGHT (kg)") = 3

'' Remarks
'iProperties.Value("Custom", "REMARKS") = ""


'    ' Part Number
'iProperties.Value("Custom", "PART NO.") = 23

'' Quantity (default = 1)
'iProperties.Value("Custom", "PART QTY") = 1

'' Description
'iProperties.Value("Custom", "DESCRIPTION") = "END PLATE FOR MANWAY SLEEVE"

'' Size (example – parameter driven)
'iProperties.Value("Custom", "SIZE") = "PL OD " & Parameter("OD") & " x " &  Parameter("ID") & " x " &  Parameter("THK") & "THK"

'' Material (from physical)
'iProperties.Value("Custom", "MATERIAL") = Parameter("MATERIAL")

'iProperties.Material = MATERIAL

'' Weight in kg
'iProperties.Value("Custom", "WEIGHT (kg)") = 1

'' Remarks
'iProperties.Value("Custom", "REMARKS") = ""

'' Part Number
'iProperties.Value("Custom", "PART NO.") = 24

'' Quantity (default = 1)
'iProperties.Value("Custom", "PART QTY") = 1

'' Description
'iProperties.Value("Custom", "DESCRIPTION") = "BOTTOM BRACKET FOR MANWAY DAVIT"

'' Size (example – parameter driven)
'iProperties.Value("Custom", "SIZE") = "PL OD " & Parameter("OD") & " x " &  Parameter("ID") & " x " &  Parameter("THK") & "THK"

'' Material (from physical)
'iProperties.Value("Custom", "MATERIAL") = Parameter("MATERIAL")

'iProperties.Material = MATERIAL

'' Weight in kg
'iProperties.Value("Custom", "WEIGHT (kg)") = 1

'' Remarks
'iProperties.Value("Custom", "REMARKS") = ""



'    ' Part Number
'iProperties.Value("Custom", "PART NO.") = 25

'' Quantity (default = 1)
'iProperties.Value("Custom", "PART QTY") = 1

'' Description
'iProperties.Value("Custom", "DESCRIPTION") = "BRASS PLATE FOR DAVIT"

'' Size (example – parameter driven)
'iProperties.Value("Custom", "SIZE") = "PL OD " & Parameter("OD") & " x " &  Parameter("ID") & " x " &  Parameter("THK") & "THK"

'' Material (from physical)
'iProperties.Value("Custom", "MATERIAL") = Parameter("MATERIAL")

'iProperties.Material = MATERIAL

'' Weight in kg
'iProperties.Value("Custom", "WEIGHT (kg)") = 1

'' Remarks
'iProperties.Value("Custom", "REMARKS") = ""


'' Part Number
'iProperties.Value("Custom", "PART NO.") = 26

'' Quantity (default = 1)
'iProperties.Value("Custom", "PART QTY") = 1

'' Description
'iProperties.Value("Custom", "DESCRIPTION") = "TOP BRACKET FOR MANWAY DAVIT"

'' Size (example – parameter driven)
'iProperties.Value("Custom", "SIZE") = "PL OD " & Parameter("OD") & " x ID " &  Parameter("ID") & " x " &  Parameter("THK") & "THK"

'' Material (from physical)
'iProperties.Value("Custom", "MATERIAL") = Parameter("MATERIAL")

'iProperties.Material = MATERIAL

'' Weight in kg
'iProperties.Value("Custom", "WEIGHT (kg)") = 1

'' Remarks
'iProperties.Value("Custom", "REMARKS") = ""


'' Part Number
'iProperties.Value("Custom", "PART NO.") = 27

'' Quantity (default = 1)
'iProperties.Value("Custom", "PART QTY") = 1

'' Description
'iProperties.Value("Custom", "DESCRIPTION") = "SLEEVE FOR MANWAY DAVIT"

'' Size (example – parameter driven)
'iProperties.Value("Custom", "SIZE") = "NPS 2 1/2, SCH40S - " & Parameter("LENGTH") & " LG"

'' Material (from physical)
'iProperties.Value("Custom", "MATERIAL") = Parameter("MATERIAL")

'iProperties.Material = MATERIAL

'' Weight in kg
'iProperties.Value("Custom", "WEIGHT (kg)") = 3

'' Remarks
'iProperties.Value("Custom", "REMARKS") = ""


'' Part Number
'iProperties.Value("Custom", "PART NO.") = 28

'' Quantity (default = 1)
'iProperties.Value("Custom", "PART QTY") = 2

'' Description
'iProperties.Value("Custom", "DESCRIPTION") = "GREASE FITTINGS FOR MANWAY"

'' Size (example – parameter driven)
'iProperties.Value("Custom", "SIZE") = "GREASE FITTINGS 1/4''"

'' Material (from physical)
'iProperties.Value("Custom", "MATERIAL") = Parameter("MATERIAL")

'iProperties.Material = MATERIAL

'' Weight in kg
'iProperties.Value("Custom", "WEIGHT (kg)") = 1

'' Remarks
'iProperties.Value("Custom", "REMARKS") = ""


'    ' Part Number
'iProperties.Value("Custom", "PART NO.") = 29

'' Quantity (default = 1)
'iProperties.Value("Custom", "PART QTY") = 1

'' Description
'iProperties.Value("Custom", "DESCRIPTION") = "MANWAY DAVIT ARM PIPE"

'' Size (example – parameter driven)
'iProperties.Value("Custom", "SIZE") = "NPS 2, SCH80S - 1090 LG"

'' Material (from physical)
'iProperties.Value("Custom", "MATERIAL") = Parameter("MATERIAL")

'iProperties.Material = MATERIAL

'' Weight in kg
'iProperties.Value("Custom", "WEIGHT (kg)") = 9

'' Remarks
'iProperties.Value("Custom", "REMARKS") = ""

'' Part Number
'iProperties.Value("Custom", "PART NO.") = 30

'' Quantity (default = 1)
'iProperties.Value("Custom", "PART QTY") = 1

'' Description
'iProperties.Value("Custom", "DESCRIPTION") = "MANWAY FLANGE CONNECTING ROD"

'' Size (example – parameter driven)
'iProperties.Value("Custom", "SIZE") = "ø" & Parameter("d17") & " - 300 LG"

'' Material (from physical)
'iProperties.Value("Custom", "MATERIAL") = Parameter("MATERIAL")

'iProperties.Material = MATERIAL

'' Weight in kg
'iProperties.Value("Custom", "WEIGHT (kg)") = 1

'' Remarks
'iProperties.Value("Custom", "REMARKS") = ""


'' Part Number
'iProperties.Value("Custom", "PART NO.") = 31

'' Quantity (default = 1)
'iProperties.Value("Custom", "PART QTY") = 1

'' Description
'iProperties.Value("Custom", "DESCRIPTION") = "END PLATE FOR MANWAY DAVIT ARM"

'' Size (example – parameter driven)
'iProperties.Value("Custom", "SIZE") = "PL ø " & Parameter("OD") & " x " & Parameter("THK") & "THK"

'' Material (from physical)
'iProperties.Value("Custom", "MATERIAL") = Parameter("MATERIAL")

'iProperties.Material = MATERIAL

'' Weight in kg
'iProperties.Value("Custom", "WEIGHT (kg)") = 1

'' Remarks
'iProperties.Value("Custom", "REMARKS") = ""

'' Part Number
'iProperties.Value("Custom", "PART NO.") = 32

'' Quantity (default = 1)
'iProperties.Value("Custom", "PART QTY") = 2

'' Description
'iProperties.Value("Custom", "DESCRIPTION") = "GRAB RUNG & LADDER RUNG"

'' Size (example – parameter driven)
'iProperties.Value("Custom", "SIZE") = "ø20 - 955 LG"

'' Material (from physical)
'iProperties.Value("Custom", "MATERIAL") = Parameter("MATERIAL")

'iProperties.Material = MATERIAL

'' Weight in kg
'iProperties.Value("Custom", "WEIGHT (kg)") = 5

'' Remarks
'iProperties.Value("Custom", "REMARKS") = ""

'' Part Number
'iProperties.Value("Custom", "PART NO.") = 33

'' Quantity (default = 1)
'iProperties.Value("Custom", "PART QTY") = 2

'' Description
'iProperties.Value("Custom", "DESCRIPTION") = "MANWAY HANDLE"

'' Size (example – parameter driven)
'iProperties.Value("Custom", "SIZE") = "ø" & Parameter("DIA") & "- 300 LG" 

'' Material (from physical)
'iProperties.Value("Custom", "MATERIAL") = Parameter("MATERIAL")

'iProperties.Material = MATERIAL

'' Weight in kg
'iProperties.Value("Custom", "WEIGHT (kg)") = 1

'' Remarks
'iProperties.Value("Custom", "REMARKS") = ""

'' Part Number
'iProperties.Value("Custom", "PART NO.") = 34

'' Quantity (default = 1)
'iProperties.Value("Custom", "PART QTY") = 10

'' Description
'iProperties.Value("Custom", "DESCRIPTION") = "FLANGE FOR NOZZLE N1,N2,N3,N4,N5,N6A,N6B,N8,N9 & N10"

'' Size (example – parameter driven)
'iProperties.Value("Custom", "SIZE") = "NPS 2, WNRF, 150#, SCH40S, ASME B 16.5"

'' Material (from physical)
'iProperties.Value("Custom", "MATERIAL") = Parameter("MATERIAL")

'iProperties.Material = MATERIAL

'' Weight in kg
'iProperties.Value("Custom", "WEIGHT (kg)") = 27

'' Remarks
'iProperties.Value("Custom", "REMARKS") = ""


'' Part Number
'iProperties.Value("Custom", "PART NO.") = 35

'' Quantity (default = 1)
'iProperties.Value("Custom", "PART QTY") = 3

'' Description
'iProperties.Value("Custom", "DESCRIPTION") = "PIPE FOR NOZZLE N1,N5 & N8"

'' Size (example – parameter driven)
'iProperties.Value("Custom", "SIZE") = "NPS 2, SCH40S - 352 LG"

'' Material (from physical)
'iProperties.Value("Custom", "MATERIAL") = Parameter("MATERIAL")

'iProperties.Material = MATERIAL

'' Weight in kg
'iProperties.Value("Custom", "WEIGHT (kg)") =6

'' Remarks
'iProperties.Value("Custom", "REMARKS") = ""


'' Part Number
'iProperties.Value("Custom", "PART NO.") = 36

'' Quantity (default = 1)
'iProperties.Value("Custom", "PART QTY") = 3

'' Description
'iProperties.Value("Custom", "DESCRIPTION") = "R.F PAD FOR NOZZLE-N1,N5 & N8"

'' Size (example – parameter driven)
'iProperties.Value("Custom", "SIZE") = "PL OD " & Parameter("PAD_OD") & " x ID " &  Parameter("PAD_ID") & " x " &  Parameter("PAD_THK") & "THK"

'' Material (from physical)
'iProperties.Value("Custom", "MATERIAL") = Parameter("MATERIAL")

'iProperties.Material = MATERIAL

'' Weight in kg
'iProperties.Value("Custom", "WEIGHT (kg)") = 5

'' Remarks
'iProperties.Value("Custom", "REMARKS") = ""


'' Part Number
'iProperties.Value("Custom", "PART NO.") = 37

'' Quantity (default = 1)
'iProperties.Value("Custom", "PART QTY") = 1

'' Description
'iProperties.Value("Custom", "DESCRIPTION") = "PIPE FOR NOZZLE N2"

'' Size (example – parameter driven)
'iProperties.Value("Custom", "SIZE") = "NPS 2, SCH40S - 183 LG"

'' Material (from physical)
'iProperties.Value("Custom", "MATERIAL") = Parameter("MATERIAL")

'iProperties.Material = MATERIAL

'' Weight in kg
'iProperties.Value("Custom", "WEIGHT (kg)") = 1

'' Remarks
'iProperties.Value("Custom", "REMARKS") = ""


'' Part Number
'iProperties.Value("Custom", "PART NO.") = 38

'' Quantity (default = 1)
'iProperties.Value("Custom", "PART QTY") = 1

'' Description
'iProperties.Value("Custom", "DESCRIPTION") = "R.F PAD FOR NOZZLE-N2"

'' Size (example – parameter driven)
'iProperties.Value("Custom", "SIZE") = "PL OD " & Parameter("PAD_OD") & " x ID " &  Parameter("PAD_ID") & " x " &  Parameter("PAD_THK") & "THK"

'' Material (from physical)
'iProperties.Value("Custom", "MATERIAL") = Parameter("MATERIAL")

'iProperties.Material = MATERIAL

'' Weight in kg
'iProperties.Value("Custom", "WEIGHT (kg)") = 2

'' Remarks
'iProperties.Value("Custom", "REMARKS") = ""



'' Part Number
'iProperties.Value("Custom", "PART NO.") = 39

'' Quantity (default = 1)
'iProperties.Value("Custom", "PART QTY") = 1

'' Description
'iProperties.Value("Custom", "DESCRIPTION") = "VORTEX BREAKER PLATE-1"

'' Size (example – parameter driven)
'iProperties.Value("Custom", "SIZE") = "PL " & Parameter("LENGTH") & " x " &  Parameter("WIDTH") & " x " &  Parameter("THK") & "THK"

'' Material (from physical)
'iProperties.Value("Custom", "MATERIAL") = Parameter("MATERIAL")

'iProperties.Material = MATERIAL

'' Weight in kg
'iProperties.Value("Custom", "WEIGHT (kg)") = 1

'' Remarks
'iProperties.Value("Custom", "REMARKS") = ""


'' Part Number
'iProperties.Value("Custom", "PART NO.") = 40

'' Quantity (default = 1)
'iProperties.Value("Custom", "PART QTY") = 2

'' Description
'iProperties.Value("Custom", "DESCRIPTION") = "VORTEX BREAKER PLATE-2"

'' Size (example – parameter driven)
'iProperties.Value("Custom", "SIZE") = "PL " & Parameter("LENGTH") & " x " &  Parameter("WIDTH") & " x " &  Parameter("THK") & "THK"

'' Material (from physical)
'iProperties.Value("Custom", "MATERIAL") = Parameter("MATERIAL")

'iProperties.Material = MATERIAL

'' Weight in kg
'iProperties.Value("Custom", "WEIGHT (kg)") = 1

'' Remarks
'iProperties.Value("Custom", "REMARKS") = ""


'' Part Number
'iProperties.Value("Custom", "PART NO.") = 41

'' Quantity (default = 1)
'iProperties.Value("Custom", "PART QTY") = 1

'' Description
'iProperties.Value("Custom", "DESCRIPTION") = "PIPE FOR NOZZLE N3"

'' Size (example – parameter driven)
'iProperties.Value("Custom", "SIZE") = "NPS 2, SCH40S - 107 LG"

'' Material (from physical)
'iProperties.Value("Custom", "MATERIAL") = Parameter("MATERIAL")

'iProperties.Material = MATERIAL

'' Weight in kg
'iProperties.Value("Custom", "WEIGHT (kg)") = 1

'' Remarks
'iProperties.Value("Custom", "REMARKS") = ""





'' Part Number
'iProperties.Value("Custom", "PART NO.") = 42

'' Quantity (default = 1)
'iProperties.Value("Custom", "PART QTY") = 1

'' Description
'iProperties.Value("Custom", "DESCRIPTION") = "R.F PAD FOR NOZZLE-N3"

'' Size (example – parameter driven)
'iProperties.Value("Custom", "SIZE") = "PL OD " & Parameter("PAD_OD") & " x ID " &  Parameter("PAD_ID") & " x " &  Parameter("PAD_THK") & "THK"

'' Material (from physical)
'iProperties.Value("Custom", "MATERIAL") = Parameter("MATERIAL")

'iProperties.Material = MATERIAL

'' Weight in kg
'iProperties.Value("Custom", "WEIGHT (kg)") = 2

'' Remarks
'iProperties.Value("Custom", "REMARKS") = ""


'    ' Part Number
'iProperties.Value("Custom", "PART NO.") = 43

'' Quantity (default = 1)
'iProperties.Value("Custom", "PART QTY") = 1

'' Description
'iProperties.Value("Custom", "DESCRIPTION") = "PIPE FOR NOZZLE N4"

'' Size (example – parameter driven)
'iProperties.Value("Custom", "SIZE") = "NPS 2, SCH40S - 2326 LG"

'' Material (from physical)
'iProperties.Value("Custom", "MATERIAL") = Parameter("MATERIAL")

'iProperties.Material = MATERIAL

'' Weight in kg
'iProperties.Value("Custom", "WEIGHT (kg)") = 13

'' Remarks
'iProperties.Value("Custom", "REMARKS") = ""



'' Part Number
'iProperties.Value("Custom", "PART NO.") = 44

'' Quantity (default = 1)
'iProperties.Value("Custom", "PART QTY") = 1

'' Description
'iProperties.Value("Custom", "DESCRIPTION") = "R.F PAD FOR NOZZLE-N4"

'' Size (example – parameter driven)
'iProperties.Value("Custom", "SIZE") = "PL OD " & Parameter("PAD_OD") & " x ID " &  Parameter("PAD_ID") & " x " &  Parameter("PAD_THK") & "THK"

'' Material (from physical)
'iProperties.Value("Custom", "MATERIAL") = Parameter("MATERIAL")

'iProperties.Material = MATERIAL

'' Weight in kg
'iProperties.Value("Custom", "WEIGHT (kg)") = 2

'' Remarks
'iProperties.Value("Custom", "REMARKS") = ""


'' Part Number
'iProperties.Value("Custom", "PART NO.") = 45

'' Quantity (default = 1)
'iProperties.Value("Custom", "PART QTY") =2

'' Description
'iProperties.Value("Custom", "DESCRIPTION") = "U-CLAMP FOR NOZZLE-N4"

'' Size (example – parameter driven)
'iProperties.Value("Custom", "SIZE") = "M" & Parameter("M12_DIA") & " FOR NPS 2"

'' Material (from physical)
'iProperties.Value("Custom", "MATERIAL") = Parameter("MATERIAL")

'iProperties.Material = MATERIAL

'' Weight in kg
'iProperties.Value("Custom", "WEIGHT (kg)") = 1

'' Remarks
'iProperties.Value("Custom", "REMARKS") = ""



'' Part Number
'iProperties.Value("Custom", "PART NO.") = 46

'' Quantity (default = 1)
'iProperties.Value("Custom", "PART QTY") = 2

'' Description
'iProperties.Value("Custom", "DESCRIPTION") = "PIPE SUPPORT ANGLE FOR NOZZLE-N4(L" & Parameter("d0") & "x" & Parameter("d6") & "x" & Parameter("d1") & "THK)"

'' Size (example – parameter driven)
'iProperties.Value("Custom", "SIZE") = "PL 100x" & Parameter("LENGTH") & "x"&  Parameter("d1") & "THK"

'' Material (from physical)
'iProperties.Value("Custom", "MATERIAL") = Parameter("MATERIAL")

'iProperties.Material = MATERIAL

'' Weight in kg
'iProperties.Value("Custom", "WEIGHT (kg)") = 3

'' Remarks
'iProperties.Value("Custom", "REMARKS") = ""



'' Part Number
'iProperties.Value("Custom", "PART NO.") = 47

'' Quantity (default = 1)
'iProperties.Value("Custom", "PART QTY") = 2

'' Description
'iProperties.Value("Custom", "DESCRIPTION") = "PAD FOR NOZZLE-N4 PIPE SUPPORT"

'' Size (example – parameter driven)
'iProperties.Value("Custom", "SIZE") = "PL " & Parameter("LENGTH") & "x" &  Parameter("WIDTH") &  "x" &  Parameter("THK") & "THK"

'' Material (from physical)
'iProperties.Value("Custom", "MATERIAL") = Parameter("MATERIAL")

'iProperties.Material = MATERIAL

'' Weight in kg
'iProperties.Value("Custom", "WEIGHT (kg)") = 3

'' Remarks
'iProperties.Value("Custom", "REMARKS") = ""



'' Part Number
'iProperties.Value("Custom", "PART NO.") = 48

'' Quantity (default = 1)
'iProperties.Value("Custom", "PART QTY") = 4

'' Description
'iProperties.Value("Custom", "DESCRIPTION") = "PIPE FOR NOZZLE-N6A,N6B,N9 & N10"

'' Size (example – parameter driven)
'iProperties.Value("Custom", "SIZE") = "NPS 2, SCH40S - 97 LG"

'' Material (from physical)
'iProperties.Value("Custom", "MATERIAL") = Parameter("MATERIAL")

'iProperties.Material = MATERIAL

'' Weight in kg
'iProperties.Value("Custom", "WEIGHT (kg)") = 3

'' Remarks
'iProperties.Value("Custom", "REMARKS") = ""


'' Part Number
'iProperties.Value("Custom", "PART NO.") = 49

'' Quantity (default = 1)
'iProperties.Value("Custom", "PART QTY") = 3

'' Description
'iProperties.Value("Custom", "DESCRIPTION") = "R.F PAD FOR NOZZLE-N6A,N6B & N10"

'' Size (example – parameter driven)
'iProperties.Value("Custom", "SIZE") = "PL OD " & Parameter("PAD_OD") & " x ID " &  Parameter("PAD_ID") & " x " &  Parameter("PAD_THK") & "THK"

'' Material (from physical)
'iProperties.Value("Custom", "MATERIAL") = Parameter("MATERIAL")

'iProperties.Material = MATERIAL

'' Weight in kg
'iProperties.Value("Custom", "WEIGHT (kg)") = 4

'' Remarks
'iProperties.Value("Custom", "REMARKS") = ""


'' Part Number
'iProperties.Value("Custom", "PART NO.") = 50

'' Quantity (default = 1)
'iProperties.Value("Custom", "PART QTY") =2

'' Description
'iProperties.Value("Custom", "DESCRIPTION") = "FLANGE FOR NOZZLE N7A,N7B"

'' Size (example – parameter driven)
'iProperties.Value("Custom", "SIZE") = "NPS 3, WNRF, 150#, SCH40S, ASME B 16.5"

'' Material (from physical)
'iProperties.Value("Custom", "MATERIAL") = Parameter("MATERIAL")

'iProperties.Material = MATERIAL

'' Weight in kg
'iProperties.Value("Custom", "WEIGHT (kg)") = 10

'' Remarks
'iProperties.Value("Custom", "REMARKS") = ""


'' Part Number
'iProperties.Value("Custom", "PART NO.") = 51

'' Quantity (default = 1)
'iProperties.Value("Custom", "PART QTY") = 1

'' Description
'iProperties.Value("Custom", "DESCRIPTION") = "PIPE FOR NOZZLE N7A"

'' Size (example – parameter driven)
'iProperties.Value("Custom", "SIZE") = "NPS 3, SCH40S - 346 LG"

'' Material (from physical)
'iProperties.Value("Custom", "MATERIAL") = Parameter("MATERIAL")

'iProperties.Material = MATERIAL

'' Weight in kg
'iProperties.Value("Custom", "WEIGHT (kg)") = 4

'' Remarks
'iProperties.Value("Custom", "REMARKS") = ""



'' Part Number
'iProperties.Value("Custom", "PART NO.") = 52

'' Quantity (default = 1)
'iProperties.Value("Custom", "PART QTY") = 1

'' Description
'iProperties.Value("Custom", "DESCRIPTION") = "R.F PAD FOR NOZZLE-N7A"

'' Size (example – parameter driven)
'iProperties.Value("Custom", "SIZE") = "PL OD " & Parameter("PAD_OD") & " x ID " &  Parameter("PAD_ID") & " x " &  Parameter("PAD_THK") & "THK"

'' Material (from physical)
'iProperties.Value("Custom", "MATERIAL") = Parameter("MATERIAL")

'iProperties.Material = MATERIAL

'' Weight in kg
'iProperties.Value("Custom", "WEIGHT (kg)") = 2

'' Remarks
'iProperties.Value("Custom", "REMARKS") = ""



'' Part Number
'iProperties.Value("Custom", "PART NO.") = 53

'' Quantity (default = 1)
'iProperties.Value("Custom", "PART QTY") = 1

'' Description
'iProperties.Value("Custom", "DESCRIPTION") = "PIPE FOR NOZZLE N7B"

'' Size (example – parameter driven)
'iProperties.Value("Custom", "SIZE") = "NPS 3, SCH40S - 143 LG"

'' Material (from physical)
'iProperties.Value("Custom", "MATERIAL") = Parameter("MATERIAL")

'iProperties.Material = MATERIAL

'' Weight in kg
'iProperties.Value("Custom", "WEIGHT (kg)") = 2

'' Remarks
'iProperties.Value("Custom", "REMARKS") = ""




'' Part Number
'iProperties.Value("Custom", "PART NO.") = 54

'' Quantity (default = 1)
'iProperties.Value("Custom", "PART QTY") = 1

'' Description
'iProperties.Value("Custom", "DESCRIPTION") = "R.F PAD FOR NOZZLE-N7B"

'' Size (example – parameter driven)
'iProperties.Value("Custom", "SIZE") = "PL OD " & Parameter("PAD_OD") & " x ID " &  Parameter("PAD_ID") & " x " &  Parameter("PAD_THK") & "THK"

'' Material (from physical)
'iProperties.Value("Custom", "MATERIAL") = Parameter("MATERIAL")

'iProperties.Material = MATERIAL

'' Weight in kg
'iProperties.Value("Custom", "WEIGHT (kg)") = 2

'' Remarks
'iProperties.Value("Custom", "REMARKS") = ""



'' Part Number
'iProperties.Value("Custom", "PART NO.") = 55

'' Quantity (default = 1)
'iProperties.Value("Custom", "PART QTY") = 1

'' Description
'iProperties.Value("Custom", "DESCRIPTION") = "NOZZLE STIFFENER FOR REQUIRED NOZZLES"

'' Size (example – parameter driven)
'iProperties.Value("Custom", "SIZE") = "PL 30x2700x6THK"

'' Material (from physical)
'iProperties.Value("Custom", "MATERIAL") = Parameter("MATERIAL")

'iProperties.Material = MATERIAL

'' Weight in kg
'iProperties.Value("Custom", "WEIGHT (kg)") = 4

'' Remarks
'iProperties.Value("Custom", "REMARKS") = ""




'' Part Number
'iProperties.Value("Custom", "PART NO.") = 56

'' Quantity (default = 1)
'iProperties.Value("Custom", "PART QTY") = 1

'' Description
'iProperties.Value("Custom", "DESCRIPTION") = "NAME PLATE"

'' Size (example – parameter driven)
'iProperties.Value("Custom", "SIZE") = "PL " & Parameter("LENGTH_1") & "x" & Parameter("HEIGHT_1") & "x" & Parameter("THK") & "THK"

'' Material (from physical)
'iProperties.Value("Custom", "MATERIAL") = Parameter("MATERIAL")

'iProperties.Material = MATERIAL

'' Weight in kg
'iProperties.Value("Custom", "WEIGHT (kg)") = 1

'' Remarks
'iProperties.Value("Custom", "REMARKS") = ""



'' Part Number
'iProperties.Value("Custom", "PART NO.") = 57

'' Quantity (default = 1)
'iProperties.Value("Custom", "PART QTY") = 1

'' Description
'iProperties.Value("Custom", "DESCRIPTION") = "NAME PLATE BRACKET"

'' Size (example – parameter driven)
'iProperties.Value("Custom", "SIZE") = "PL " & Parameter("LENGTH") & "x" & Parameter("HEIGHT") & "x" & Parameter("THK") & "THK"

'' Material (from physical)
'iProperties.Value("Custom", "MATERIAL") = Parameter("MATERIAL")

'iProperties.Material = MATERIAL

'' Weight in kg
'iProperties.Value("Custom", "WEIGHT (kg)") = 3

'' Remarks
'iProperties.Value("Custom", "REMARKS") = ""




'' Part Number
'iProperties.Value("Custom", "PART NO.") = 58

'' Quantity (default = 1)
'iProperties.Value("Custom", "PART QTY") = 1

'' Description
'iProperties.Value("Custom", "DESCRIPTION") = "STIFFENER FOR NAME PLATE BRACKET"

'' Size (example – parameter driven)
'iProperties.Value("Custom", "SIZE") = "PL " & Parameter("LENGTH") & "x" & Parameter("HEIGHT") & "x" & Parameter("THK") & "THK"

'' Material (from physical)
'iProperties.Value("Custom", "MATERIAL") = Parameter("MATERIAL")

'iProperties.Material = MATERIAL

'' Weight in kg
'iProperties.Value("Custom", "WEIGHT (kg)") = 2

'' Remarks
'iProperties.Value("Custom", "REMARKS") = ""



'' Part Number
'iProperties.Value("Custom", "PART NO.") = 59

'' Quantity (default = 1)
'iProperties.Value("Custom", "PART QTY") = 2

'' Description
'iProperties.Value("Custom", "DESCRIPTION") = "MSDS BOX MOUNTING PLATE-2"

'' Size (example – parameter driven)
'iProperties.Value("Custom", "SIZE") = "PL " & Parameter("d1") & "x" & Parameter("d0") & "x" & Parameter("d2") & "THK"

'' Material (from physical)
'iProperties.Value("Custom", "MATERIAL") = Parameter("MATERIAL")

'iProperties.Material = MATERIAL

'' Weight in kg
'iProperties.Value("Custom", "WEIGHT (kg)") = 1

'' Remarks
'iProperties.Value("Custom", "REMARKS") = ""


'' Part Number
'iProperties.Value("Custom", "PART NO.") = 60

'' Quantity (default = 1)
'iProperties.Value("Custom", "PART QTY") = 1

'' Description
'iProperties.Value("Custom", "DESCRIPTION") = "MSDS BOX MOUNTING PLATE-1"

'' Size (example – parameter driven)
'iProperties.Value("Custom", "SIZE") = "PL " & Parameter("d0") & "x" & Parameter("d1") & "x" & Parameter("d2") & "THK"

'' Material (from physical)
'iProperties.Value("Custom", "MATERIAL") = Parameter("MATERIAL")

'iProperties.Material = MATERIAL

'' Weight in kg
'iProperties.Value("Custom", "WEIGHT (kg)") = 3

'' Remarks
'iProperties.Value("Custom", "REMARKS") = ""




'' Part Number
'iProperties.Value("Custom", "PART NO.") = 62

'' Quantity (default = 1)
'iProperties.Value("Custom", "PART QTY") = 1

'' Description
'iProperties.Value("Custom", "DESCRIPTION") = "R.F PAD FOR NOZZLE-N9"

'' Size (example – parameter driven)
'iProperties.Value("Custom", "SIZE") = "PL OD " & Parameter("PAD_OD") & " x ID " &  Parameter("PAD_ID") & " x " &  Parameter("PAD_THK") & "THK"

'' Material (from physical)
'iProperties.Value("Custom", "MATERIAL") = Parameter("MATERIAL")

'iProperties.Material = MATERIAL

'' Weight in kg
'iProperties.Value("Custom", "WEIGHT (kg)") = 1

'' Remarks
'iProperties.Value("Custom", "REMARKS") = ""



'    ' Part Number
'iProperties.Value("Custom", "PART NO.") = 64

'' Quantity (default = 1)
'iProperties.Value("Custom", "PART QTY") = 1

'' Description
'iProperties.Value("Custom", "DESCRIPTION") = "IMPULSE PLATE FOR NOZZLE-N4"

'' Size (example – parameter driven)
'iProperties.Value("Custom", "SIZE") = "PL ø " & Parameter("ARC_LENGTH") & "x" & Parameter("THK") & "THK"

'' Material (from physical)
'iProperties.Value("Custom", "MATERIAL") = Parameter("MATERIAL")

'iProperties.Material = MATERIAL

'' Weight in kg
'iProperties.Value("Custom", "WEIGHT (kg)") = 2

'' Remarks
'iProperties.Value("Custom", "REMARKS") = ""




















'End Module
