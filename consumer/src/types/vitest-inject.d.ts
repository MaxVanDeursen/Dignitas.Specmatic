import 'vitest';

declare module 'vitest' {
  export function inject(key: 'port'): number;
}