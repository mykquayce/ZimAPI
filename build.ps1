docker buildx build `
	--file '.\ZimApi.WebApplication\Dockerfile' `
	--platform 'linux/arm64,linux/amd64' `
	--pull `
	--push `
	--tag 'eassbhhtgu/zimapi:latest' `
	.\ZimApi.WebApplication\
