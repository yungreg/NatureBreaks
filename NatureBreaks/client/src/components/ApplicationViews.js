import React from "react";
import { Routes, Route, Navigate } from "react-router-dom";
import VideoList from "./VideoList";
import Login from "./Login";
import FavoriteBreaks from "./FavoriteBreaks";
import Register from "./Register";
import VideoAddForm from "./VideoAddForm";
import VideoEditor from "./VideoEditor.js";

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
        <Route path="favoritebreaks/:userid" element={<FavoriteBreaks />} />
        <Route path="videolist" element={<VideoList />} />
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
