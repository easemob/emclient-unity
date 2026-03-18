require 'xcodeproj'

project_path = ARGV[0]
old_framework_name = ARGV[1]
new_framework_name = ARGV[2]
new_framework_path = "../#{new_framework_name}"
old_framework_path = "../#{old_framework_name}"

if project_path.nil? || new_framework_path.nil? || old_framework_path.nil?
  puts "Usage: ruby script.rb <project_path> <old_framework_name> <new_framework_name>"
  exit 1
end

project = Xcodeproj::Project.open(project_path)
old_file_ref = project.files.find { |file| file.path == old_framework_path }

if old_file_ref
  new_file_ref = project.new_file(new_framework_path)

  project.targets.each do |target|
    target.build_phases.each do |phase|
      if phase.isa == 'PBXFrameworksBuildPhase'
        phase.files.each do |build_file|
          if build_file.file_ref == old_file_ref
            build_file.file_ref = new_file_ref
          end
        end
      end
    end
  end

  old_file_ref.remove_from_project
  project.save
  puts "Successfully replaced #{old_framework_path} with #{new_framework_path}"
else
  puts "Old framework not found: #{old_framework_path}"
end
