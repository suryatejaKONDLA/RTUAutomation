window.AgGridInterop = {
    renderGrid: function (divId, data) {
        const container = document.getElementById(divId);
        if (!container || !Array.isArray(data) || data.length === 0) {
            console.error("Grid div missing or data invalid.");
            return;
        }

        const columnDefs = Object.keys(data[0]).map(k => ({
            field: k,
            editable: true,
            valueParser: params => String(params.newValue ?? null)
        }));


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
