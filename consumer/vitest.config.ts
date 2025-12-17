// vitest.config.ts
import { defineConfig } from 'vitest/config';

export default defineConfig({
    test: {
        globals: true,
        globalSetup: ['./src/utils/testcontainers.ts'],
    },
});