import React, { useEffect, useState } from "react";
import Video from "./Video";
import { getAllVideos } from "../modules/videoManager";
import { Col, Container, Row } from "reactstrap";

const VideoList = ({ user }) => {
  const [videos, setVideos] = useState([]);
  const [currentUser, setCurrentUser] = useState({});

  const getVideos = () => {
    getAllVideos().then((videos) => setVideos(videos));
  };

  useEffect(() => {
    getVideos();
    setCurrentUser(user);
  }, []);

  return (
    <Container className="container">
      <Row>
        <Col>
          <div className="row justify-content-center">
            {videos.map((video) => (
              <Video user={user} video={video} key={video.id} />
            ))}
          </div>
        </Col>
      </Row>
    </Container>
  );
};

export default VideoList;

// create 4 selectboxes
