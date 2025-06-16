window.readExcelSheets = async function (fileUrl) {
    try {
        const response = await fetch(fileUrl);
        const arrayBuffer = await response.arrayBuffer();
        const data = new Uint8Array(arrayBuffer);
        const workbook = XLSX.read(data, { type: 'array' });

        return workbook.SheetNames;
    } catch (error) {
        console.error("Error reading Excel file:", error);
        return [];
    }
};

//window.readExcelSheetData = async function (fileUrl, sheetName) {
//    try {
//        const response = await fetch(fileUrl);
//        const arrayBuffer = await response.arrayBuffer();
//        const data = new Uint8Array(arrayBuffer);
//        const workbook = XLSX.read(data, { type: 'array' });

//        const sheet = workbook.Sheets[sheetName];
//        if (!sheet) throw new Error("Sheet not found");

//        // Fill merged cells
//        const merges = sheet["!merges"] || [];
//        merges.forEach(merge => {
//            const start = XLSX.utils.encode_cell(merge.s);
//            const value = sheet[start]?.v;

//            for (let R = merge.s.r; R <= merge.e.r; ++R) {
//                for (let C = merge.s.c; C <= merge.e.c; ++C) {
//                    const cell = XLSX.utils.encode_cell({ r: R, c: C });
//                    if (!sheet[cell]) {
//                        sheet[cell] = { t: 's', v: value };
//                    }
//                }
//            }
//        });

//        // Parse as raw 2D array (no headers)
//        let json = XLSX.utils.sheet_to_json(sheet, { header: 1 });

//        // Skip top 3 rows
//        json = json.slice(3);

//        // Fill merged J -> K column (optional logic)
//        for (let row of json) {
//            if (row[9] && (!row[10] || row[10] === "")) {
//                row[10] = row[9];
//            }
//        }

//        // Remove fully empty rows
//        json = json.filter(row => row.some(cell => cell !== null && cell !== ""));

//        // Normalize to string
//        const cleanData = json.map(row =>
//            row.map(cell => (cell != null ? cell.toString().trim() : ""))
//        );

//        return JSON.stringify(cleanData);
//    } catch (error) {
//        console.error("Error reading Excel sheet:", error);
//        return "[]";
//    }
//};

window.readExcelSheetData = async function (fileUrl, sheetName) {
    try {
        const response = await fetch(fileUrl);
        const arrayBuffer = await response.arrayBuffer();
        const data = new Uint8Array(arrayBuffer);
        const workbook = XLSX.read(data, { type: 'array' });

        const sheet = workbook.Sheets[sheetName];
        if (!sheet) throw new Error("Sheet not found");

        // Use first row as headers
        const keyValueObjects = XLSX.utils.sheet_to_json(sheet, { defval: "" });

        return JSON.stringify(keyValueObjects);
    } catch (error) {
        console.error("Error reading Excel sheet:", error);
        return "[]";
    }
};


window.downloadJsonFile = function (fileName, jsonContent) {
    const blob = new Blob([jsonContent], { type: "application/json" });
    const url = URL.createObjectURL(blob);

    const anchor = document.createElement("a");
    anchor.href = url;
    anchor.download = fileName;
    anchor.click();

    URL.revokeObjectURL(url);
};

window.readLocalJson = async function (path) {
    const response = await fetch(path);
    if (!response.ok) {
        throw new Error(`Could not fetch ${path}: ${response.status}`);
    }
    return await response.text();
};

