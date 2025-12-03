import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

export default defineConfig({
    plugins: [react()],
    base: './',
    server: {
        port: 5173,
    },
    build: {
        rollupOptions: {
            output: {
                entryFileNames: 'assets/[name].js',
                chunkFileNames: 'assets/[name]-[hash].js',
                assetFileNames: (assetInfo) => {
                    if (assetInfo.name === 'index.css') {
                        return 'assets/[name].[ext]';
                    }
                    return 'assets/[name]-[hash].[ext]';
                }
            }
        }
    }
})
