import React from "react";
import { Routes, Route, Navigate } from "react-router-dom";
import VideoList from "./VideoList";
import Video from "./Video";
import Login from "./Login";
import Register from "./Register";

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
        <Route path="register" element={<Register />} />
        <Route
          path="*"
          element={
            <p>
              Lost? let's get you back home! (insert a link to teh search page
              here)😅
            </p>
          }
        />
      </Route>
    </Routes>
  );
}
