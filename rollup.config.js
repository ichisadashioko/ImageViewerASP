import pkg from './package.json';
import typescript from 'rollup-plugin-typescript';
import resolve from 'rollup-plugin-node-resolve';

const extensions = [
    ".js", ".jsx", ".ts", ".tsx"
];
export default {
    input: './ImageViewerASP/Scripts/src/Index.tsx',
    output: [
        {
            name: 'index',
            file: './ImageViewerASP/Scripts/dist/index.esm.js',
            format: 'esm',
        }
        {
            name: 'index',
            file: './ImageViewerASP/Scripts/dist/index.cjs.js',
            format: 'cjs',
        }
    ],
    plugins: [
        resolve({ extensionss }),
        typescript()
    ]
}