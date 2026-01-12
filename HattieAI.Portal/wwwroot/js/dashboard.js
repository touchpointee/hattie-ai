window.dashboardChart = {
    instances: {},

    renderTrafficChart: function (canvasId, labels, dataSessions, dataMessages) {
        this.destroyChart(canvasId);
        const ctx = document.getElementById(canvasId);
        if (!ctx) return;

        // Gradient for Sessions
        const gradientSessions = ctx.getContext('2d').createLinearGradient(0, 0, 0, 400);
        gradientSessions.addColorStop(0, 'rgba(55, 48, 163, 0.4)'); // Indigo 800
        gradientSessions.addColorStop(1, 'rgba(55, 48, 163, 0.0)');

        // Gradient for Messages
        const gradientMessages = ctx.getContext('2d').createLinearGradient(0, 0, 0, 400);
        gradientMessages.addColorStop(0, 'rgba(99, 102, 241, 0.4)'); // Indigo 500
        gradientMessages.addColorStop(1, 'rgba(99, 102, 241, 0.0)');

        this.instances[canvasId] = new Chart(ctx, {
            type: 'line',
            data: {
                labels: labels,
                datasets: [
                    {
                        label: 'Active Sessions',
                        data: dataSessions,
                        borderColor: '#3730a3',
                        backgroundColor: gradientSessions,
                        borderWidth: 2,
                        tension: 0.4,
                        fill: true,
                        pointRadius: 0,
                        pointHoverRadius: 6
                    },
                    {
                        label: 'Total Messages',
                        data: dataMessages,
                        borderColor: '#6366f1',
                        backgroundColor: gradientMessages,
                        borderWidth: 2,
                        tension: 0.4,
                        fill: true,
                        pointRadius: 0,
                        pointHoverRadius: 6
                    }
                ]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                plugins: {
                    legend: {
                        position: 'top',
                        align: 'end',
                        labels: { usePointStyle: true, boxWidth: 8 }
                    },
                    tooltip: {
                        mode: 'index',
                        intersect: false,
                        backgroundColor: '#1e293b',
                        titleColor: '#f1f5f9',
                        bodyColor: '#f1f5f9',
                        cornerRadius: 8
                    }
                },
                scales: {
                    y: {
                        beginAtZero: true,
                        grid: { color: 'rgba(0,0,0,0.05)' }
                    },
                    x: {
                        grid: { display: false }
                    }
                },
                interaction: {
                    mode: 'nearest',
                    axis: 'x',
                    intersect: false
                }
            }
        });
    },

    renderUsageChart: function (canvasId, data) {
        this.destroyChart(canvasId);
        const ctx = document.getElementById(canvasId);
        if (!ctx) return;

        this.instances[canvasId] = new Chart(ctx, {
            type: 'doughnut',
            data: {
                labels: ['User Messages', 'AI Responses', 'System/Other'],
                datasets: [{
                    data: data,
                    backgroundColor: ['#3730a3', '#6366f1', '#94a3b8'],
                    borderWidth: 0,
                    hoverOffset: 4
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                cutout: '75%',
                plugins: {
                    legend: {
                        position: 'bottom',
                        labels: { usePointStyle: true, padding: 20 }
                    }
                }
            }
        });
    },

    renderSparkline: function (canvasId, data, color) {
        this.destroyChart(canvasId);
        const ctx = document.getElementById(canvasId);
        if (!ctx) return;

        this.instances[canvasId] = new Chart(ctx, {
            type: 'line',
            data: {
                labels: data.map((_, i) => i), // Dummy labels
                datasets: [{
                    data: data,
                    borderColor: color,
                    borderWidth: 2,
                    tension: 0.3,
                    pointRadius: 0,
                    fill: false
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                plugins: { legend: { display: false }, tooltip: { enabled: false } },
                scales: {
                    x: { display: false },
                    y: { display: false, min: Math.min(...data) - 5, max: Math.max(...data) + 5 }
                },
                layout: { padding: 0 }
            }
        });
    },

    destroyChart: function (canvasId) {
        if (this.instances[canvasId]) {
            this.instances[canvasId].destroy();
            delete this.instances[canvasId];
        }
    }
};
