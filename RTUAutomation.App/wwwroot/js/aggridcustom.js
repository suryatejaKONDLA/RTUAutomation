window.AgGridInterop = {
    renderGrid: function (divId, data) {
        const container = document.getElementById(divId);
        if (!container || !Array.isArray(data) || data.length === 0) {
            console.error("Grid div missing or data invalid.");
            return;
        }

        const unitOptions = [
            "AMPS", "VOLTS", "MW", "MV", "THD", "KF", "°", "PPM"
        ];
        const pHOptions = [
            "A", "B", "C", "N"
        ];
        const statusDescription = [
            "Off/On", "Disable/Enable", "Remote/Local", "Local/Remote", "Normal/Alarm", "Alarm/Normal", "Tripped/Closed", "Open/Closed", "Closed/Open", "FDR/TIE", "Set/Reset"];

        const columnDefs = Object.keys(data[0]).map(k => {
            const colDef = {
                field: k,
                editable: true,
                valueParser: params => String(params.newValue ?? null)
            };

            if (k === "Unit") {
                colDef.cellEditor = 'agSelectCellEditor';
                colDef.cellEditorParams = {
                    values: unitOptions
                };
            }
            if (k === "Ph") {
                colDef.cellEditor = 'agSelectCellEditor';
                colDef.cellEditorParams = {
                    values: pHOptions
                };
            }
            if (k === "Status Description") {
                colDef.cellEditor = 'agSelectCellEditor';
                colDef.cellEditorParams = {
                    values: statusDescription
                };
            }

            return colDef;
        });



        container.innerHTML = '';

        const gridOptions = {
            columnDefs,
            rowData: data,
            defaultColDef: {
                sortable: true,
                filter: true,
                resizable: true,
                editable: true
            },
            cellSelection: {
                handle: {
                    mode: 'fill',
                    direction: 'xy',
                    suppressClearOnFillReduction: true
                },
                suppressMultiRanges: false
            },

            undoRedoCellEditing: true,
            undoRedoCellEditingLimit: 100,
            autoSizeStrategy: { type: 'fitCellContents' },

            onFirstDataRendered: (params) => {
                if (params.columnApi) {
                    const allCols = params.columnApi.getAllColumns();
                    if (allCols) {
                        const colIds = allCols.map(col => col.getId());
                        params.columnApi.autoSizeColumns(colIds, false);
                    }
                }
            },

            onModelUpdated: (params) => {
                if (params.columnApi) {
                    const allCols = params.columnApi.getAllColumns();
                    if (allCols) {
                        const colIds = allCols.map(col => col.getId());
                        params.columnApi.autoSizeColumns(colIds, false);
                    }
                }
            },

            onGridSizeChanged: (params) => {
                if (params.columnApi) {
                    const allCols = params.columnApi.getAllColumns();
                    if (allCols) {
                        const colIds = allCols.map(col => col.getId());
                        params.columnApi.autoSizeColumns(colIds, false);
                    }
                }
            }

        };

        agGrid.createGrid(container, gridOptions);
    }
};
