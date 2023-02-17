import React from "react";
import { Routes, Route, Navigate } from "react-router-dom";
import VideoList from "./VideoList";
import Login from "./Login";
import FavoriteBreaks from "./FavoriteBreaks";
import Register from "./Register";
import VideoAddForm from "./VideoAddForm";
import VideoEditor from "./VideoEditor.js";

export default function ApplicationViews({ isLoggedIn, user }) {
  return (
    <Routes>
      <Route path="/">
        <Route
          index
          element={
            isLoggedIn ? <VideoList user={user} /> : <Navigate to="/login" />
          }
        />
        {/* <Route
          path="add"
          element={isLoggedIn ? <QuoteAddForm /> : <Navigate to="/login" />}
        /> */}

        <Route path="login" element={<Login />} />
        <Route path="favoritebreaks" element={<FavoriteBreaks user={user} />} />

        <Route path="register" element={<Register />} />
        <Route path="add" element={<VideoAddForm />} />
        <Route path="video/:videoId" element={<VideoEditor />} />

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
