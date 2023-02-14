// thsi will set the homepge url
const baseUrl = "/api/video";

export const getAllVideos = () => {
  return fetch(baseUrl).then((res) => res.json());
};

export const addVideo = (vid) => {
  return fetch(baseUrl, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(vid),
  });
};
