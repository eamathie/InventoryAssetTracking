import React, { useContext } from "react";
import { useNavigate } from "react-router";
import { AuthContext } from "../../tools/AuthProvider";

const Navbar: React.FC = () => {
    const navigate = useNavigate()

    const auth = useContext(AuthContext)

    const roles = auth?.roles ?? []
    const admin = roles.includes("Admin")

    const handleSignInClicked = () => {
        navigate("/auth")
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
            <div className="text-gray-600 text-sm font-medium hidden md:flex space-x-6">
                <a href="/assets" className="hover:text-gray-900">
                Assets
                </a>
                <a href="/categories" className="hover:text-gray-900">
                Categories
                </a>
                <a href="#" className="hover:text-gray-900">
                About
                </a>
                {admin && 
                <a href="/admin" className="hover:text-gray-900">
                    Admin
                </a>}
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
        <div className="text-gray-700 text-base font-medium md:hidden border-t border-gray-200">
            <div className="space-y-1 px-4 py-3">
                <a href="/assets" className="block hover:bg-gray-100 rounded-md px-3 py-2">
                    Assets
                </a>
                <a href="/categories" className="block hover:bg-gray-100 rounded-md px-3 py-2">
                    Categories
                </a>
                <a href="#" className="block hover:bg-gray-100 rounded-md px-3 py-2">
                    About
                </a>
                {admin && 
                <a href="/admin" className="block hover:bg-gray-100 rounded-md px-3 py-2">
                    Admin
                </a>}
                <button onClick={handleSignInClicked} className="w-full rounded-md bg-indigo-600 px-3 py-2 text-base font-medium text-white hover:bg-indigo-700">
                    Sign in
                </button>
            </div>
        </div>
        </nav>
    );
};

export default Navbar;

