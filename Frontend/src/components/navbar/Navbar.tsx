import React from "react";
import { useNavigate } from "react-router";

const Navbar: React.FC = () => {

    const navigate = useNavigate()
    
    const handleSignInClicked = () => {
        navigate("/Auth")
    }


    return (
        <nav className="bg-white border-b border-gray-200">
        <div className="mx-auto max-w-7xl px-4 sm:px-6 lg:px-8">
            <div className="flex h-16 items-center justify-between">
            {/* Left: Logo */}
            <div className="flex items-center">
                <span className="text-xl font-semibold text-gray-900">
                Inventory Tracker
                </span>
            </div>

            {/* Center: Links */}
            <div className="hidden md:flex space-x-6">
                <a href="#" className="text-gray-600 hover:text-gray-900 text-sm font-medium">
                Home
                </a>
                <a href="#" className="text-gray-600 hover:text-gray-900 text-sm font-medium">
                About
                </a>
                <a href="#" className="text-gray-600 hover:text-gray-900 text-sm font-medium">
                Blog
                </a>
                <a href="#" className="text-gray-600 hover:text-gray-900 text-sm font-medium">
                Contact
                </a>
            </div>

            {/* Right: Button */}
            <div onClick={handleSignInClicked} className="hidden md:flex">
                <button className="rounded-md bg-indigo-600 px-4 py-2 text-sm font-medium text-white hover:bg-indigo-700">
                Sign in
                </button>
            </div>

            {/* Mobile menu button */}
            <div className="md:hidden">
                <button
                type="button"
                className="inline-flex items-center justify-center rounded-md p-2 text-gray-600 hover:bg-gray-100 hover:text-gray-900"
                >
                <span className="sr-only">Open main menu</span>
                {/* Icon: hamburger */}
                <svg
                    className="h-6 w-6"
                    xmlns="http://www.w3.org/2000/svg"
                    fill="none"
                    viewBox="0 0 24 24"
                    stroke="currentColor"
                >
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={1.5} d="M4 6h16M4 12h16M4 18h16" />
                </svg>
                </button>
            </div>
            </div>
        </div>

        {/* Mobile menu (static example) */}
        <div className="md:hidden border-t border-gray-200">
            <div className="space-y-1 px-4 py-3">
            <a href="#" className="block text-gray-700 hover:bg-gray-100 rounded-md px-3 py-2 text-base font-medium">
                Home
            </a>
            <a href="#" className="block text-gray-700 hover:bg-gray-100 rounded-md px-3 py-2 text-base font-medium">
                About
            </a>
            <a href="#" className="block text-gray-700 hover:bg-gray-100 rounded-md px-3 py-2 text-base font-medium">
                Blog
            </a>
            <a href="#" className="block text-gray-700 hover:bg-gray-100 rounded-md px-3 py-2 text-base font-medium">
                Contact
            </a>
            <button className="w-full rounded-md bg-indigo-600 px-3 py-2 text-base font-medium text-white hover:bg-indigo-700">
                Sign in
            </button>
            </div>
        </div>
        </nav>
    );
};

export default Navbar;

