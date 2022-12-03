export async function setQRCode(image, imageStream) {
    const arrayBuffer = await imageStream.arrayBuffer();
    const blob = new Blob([arrayBuffer]);
    const url = URL.createObjectURL(blob);
    image.onload = () => {
        URL.revokeObjectURL(url);
    }
    image.src = url;
}
