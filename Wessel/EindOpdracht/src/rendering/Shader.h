#pragma once
#include <string>
#include <unordered_map>
#include "glm/glm.hpp"

class Shader {
private:
	unsigned int m_RendererID;
	std::unordered_map<std::string, int> m_uniformLocationCashe;


public:
	Shader(const std::string& filepath);
	~Shader();

	

	void Bind() const;
	void UnBind() const;

	//Set uniforms
	void SetUniform4f(const std::string& name, float v0, float v1, float v2, float v3);
	void SetUniform1i(const std::string& name, int value);
	void SetUniformMat4f(const std::string& name, const glm::mat4& matrix);



private:
	bool CompileShader();
	int GetUniformLocation(const std::string& name);
	unsigned int CompileShader(unsigned int type, const std::string & source);
	unsigned int CreateShader(const std::string & vertexShader, const std::string & fragmentShader);

};