import React from "react";
import { Routes, Route, Navigate } from "react-router-dom";
import VideoList from "./VideoList";
import Login from "./Login";
import Register from "./Register";
import VideoAddForm from "./VideoAddForm";

export default function ApplicationViews({ isLoggedIn }) {
  return (
    <Routes>
      <Route path="/">
        <Route
          index
          element={isLoggedIn ? <VideoList /> : <Navigate to="/login" />}
        />
        {/* <Route
          path="add"
          element={isLoggedIn ? <QuoteAddForm /> : <Navigate to="/login" />}
        /> */}
        <Route path="login" element={<Login />} />
        <Route path="videolist" element={<VideoList />} />
        <Route path="register" element={<Register />} />
        <Route path="add" element={<VideoAddForm />} />
        <Route
          path="*"
          element={
            <p>
              Lost? let's get you back home..... (insert a link to the search
              page here)😅
            </p>
          }
        />
      </Route>
    </Routes>
  );
}
