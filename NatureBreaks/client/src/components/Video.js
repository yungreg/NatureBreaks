import React from "react";
import { Link } from "react-router-dom";
import { Card, CardBody, Button } from "reactstrap";
import VideoEditor from "./VideoEditor.js";

/*
todo: make a button here that allows you to save a video to a profile
*/

const Video = ({ video, user }) => {
  const deleteButton = () => {
    return (
      <Button
        color="danger"
        outline
        className="session__btn-delete"
        onClick={() => {
          fetch(`https://localhost:5001/api/video/${video.id}`, {
            method: "DELETE",
          })
            .then((response) => response.json)
            .then(() => {
              window.alert("Break deleted! Thanks!");
            });
        }}
      >
        Delete This Break?
      </Button>
    );
  };
  return (
    <Card>
      <Link to={`/video/${video.id}`}>
        {/* link will go here that takes you to the break page for deleting */}
        <strong>{video.videoName}</strong>
      </Link>
      <CardBody>
        <iframe
          className="video"
          width="560"
          height="315"
          src={video.videoUrl}
          title="YouTube video player"
          frameBorder="0"
          allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share"
          allowFullScreen
        ></iframe>
        {deleteButton()}
        <div>
          <Button
            color="success"
            onClick={() => {
              fetch(`https://localhost:5001/api/favoritevideos/`, {
                method: "POST",
                headers: {
                  "Content-Type": "application/json",
                },
                body: JSON.stringify({
                  userId: user?.id,
                  videoId: video?.id,
                }),
              })
                // .then((response) => response.json())
                .then(() => {
                  window.alert("Break Favorited! Yahoo!");
                });
            }}
          >
            Favorite This Break?
          </Button>
        </div>
      </CardBody>
    </Card>
  );
};

export default Video;
