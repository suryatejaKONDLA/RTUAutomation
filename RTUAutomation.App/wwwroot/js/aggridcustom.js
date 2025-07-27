window.AgGridInterop = {
    renderGrid: function (divId, data) {
        const container = document.getElementById(divId);
        if (!container || !Array.isArray(data) || data.length === 0) {
            console.error("Grid div missing or data invalid.");
            return;
        }

        const columnDefs = Object.keys(data[0]).map(k => ({ field: k }));

        container.innerHTML = ''; // Clear existing grid

        const gridOptions = {
            columnDefs,
            rowData: data,
            defaultColDef: {
                sortable: true,
                filter: true,
                resizable: true
            },
            autoSizeStrategy: { type: 'fitCellContents' },

            // Trigger auto-size after events
            onFirstDataRendered: (params) => {
                const allCols = params.columnApi.getAllColumns();
                if (allCols) {
                    const colIds = allCols.map(col => col.getId());
                    params.columnApi.autoSizeColumns(colIds, false);
                }
            },
            onModelUpdated: (params) => {
                const allCols = params.columnApi.getAllColumns();
                if (allCols) {
                    const colIds = allCols.map(col => col.getId());
                    params.columnApi.autoSizeColumns(colIds, false);
                }
            },
            onGridSizeChanged: (params) => {
                const allCols = params.columnApi.getAllColumns();
                if (allCols) {
                    const colIds = allCols.map(col => col.getId());
                    params.columnApi.autoSizeColumns(colIds, false);
                }
            }
        };

        agGrid.createGrid(container, gridOptions);
    }
};
