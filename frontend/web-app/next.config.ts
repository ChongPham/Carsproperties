import withFlowbiteReact from "flowbite-react/plugin/nextjs";
import { NextConfig } from "next";

/** @type {import('next').NextConfig} */
const nextConfig: NextConfig = {
    logging: {
        fetches: {
            fullUrl: true
        }
    },
    images: {
        remotePatterns: [
            {protocol: 'https', hostname: 'cdn.pixabay.com'}
        ]
    }
};

export default withFlowbiteReact(nextConfig);